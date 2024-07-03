using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.OrganizationUserServices;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using ProjectClock.MVC.Extensions;
using Position = ProjectClock.Database.Entities.Position;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationServices _organizationServices;
        private IUserServices _userServices;
        private IAccountServices _accountService;
        private IOrganizationUserServices _organizationUserServices;
        private ProjectClockDbContext _projectClockDbContext;
        private IMapper _mapper;

        public OrganizationController(IOrganizationServices organizationServices,
            IUserServices userServices,
            IAccountServices accountService,
            IOrganizationUserServices organizationUserServices,
            ProjectClockDbContext projectClockDbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _userServices = userServices;
            _organizationServices = organizationServices;
            _accountService = accountService;
            _organizationUserServices = organizationUserServices;
            _projectClockDbContext = projectClockDbContext;
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrganizationDto organizationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "You didn't enter name of organization.";
                    return View();
                }


                HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
                organizationDto.UserId = await _accountService.GetUserIdFromAccountId(accountId);

                if (_organizationUserServices.IsUserAnOwner(organizationDto.UserId))
                {
                    TempData["ErrorMessage"] = "You are already an owner of organization. You can only be owner of one organization.";
                }
                else
                {
                    bool created = await _organizationServices.Create(organizationDto);

                    if (created)
                    {
                        TempData["SuccessMessage"] = "Organization created successfully.";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "This organization already exists.";
                    }
                }

                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error occurred while creating organization: {ex.Message}";
                return View();
            }
        }

        // GET: OrganizationController/Delete/5
        public async Task<IActionResult> Delete()
        {
            DeleteOrganizationDto model = new();

            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);

            var userOrganizations = await _organizationUserServices.GetUserOrganizations(userId);

            var organizationDtoList = userOrganizations.Select(x => new OrganizationDto()
            {
                OrganizationId = x.Id,
                OrganizationName = x.Name
            }).ToList();

            model.Organizations = organizationDtoList;

            return View("Delete", model);
        }

        // POST: OrganizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int organizationId)
        {
            try
            {
                bool deleted = await _organizationServices.Delete(organizationId);

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Organization deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "This organization doesn't exists.";
                }

                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Manage()
        {
            ManageOrganizationDtoRefactor model = new ManageOrganizationDtoRefactor();

            #region UserIdGetter
            /* pobranie id użytkownika */
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);
            #endregion

            #region GettingOrganizationNamesAndIdsToDto
            /* pobranie organizacji użytkownika */
            var organizations = await _organizationUserServices.GetUserAsAOwnerOrganization(userId);

            /* wyselekcjonowanie nazw i id organizacji uzytkownia */
            List<string> organizationNames = organizations.Select(o => o.Name).ToList();
            List<int> organizationIds = organizations.Select(o => o.Id).ToList();

            /* przypisanie nazw i id do modelu */
            model.OrganizationNames = organizationNames;
            model.OrganizationIds = organizationIds;


            #endregion

            #region ChooseOrganizationDtoLoading
            /* zaladowanie do dto ChooseOrganizationDto nazw i id w celu wyswietlenia listy i pobrania id wybranej organizacji */
            var chooseOragnizationDtoList = new List<ChooseOrganizationDto>();

            for (int i = 0; i < organizationNames.Count; i++)
            {
                ChooseOrganizationDto chooseOrganizationDto = new ChooseOrganizationDto()
                {
                    OrganizationId = organizationIds[i],
                    OrganizationName = organizationNames[i]
                };

                chooseOragnizationDtoList.Add(chooseOrganizationDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseOrganizations = chooseOragnizationDtoList;
            #endregion

            return View("Manage", model);
        }

        [HttpPost]
        public async Task<IActionResult> Choose(int organizationId)
        {
            var model = new ManageOrganizationDtoRefactor
            {
                SelectedOrganizationId = organizationId
            };

            #region UserIdGetter
            /* pobranie id użytkownika */
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);
            #endregion

            #region GettingOrganizationNamesAndIdsToDto
            /* pobranie organizacji użytkownia */
            var organizations = await _organizationUserServices.GetUserOrganizations(userId);

            /* wyselekcjonowanie nazw i id organizacji uzytkownia */
            List<string> organizationNames = organizations.Select(o => o.Name).ToList();
            List<int> organizationIds = organizations.Select(o => o.Id).ToList();

            /* przypisanie nazw i id do modelu */
            model.OrganizationNames = organizationNames;
            model.OrganizationIds = organizationIds;


            #endregion

            #region ChooseOrganizationDtoLoading
            /* zaladowanie do dto ChooseOrganizationDto nazw i id w celu wyswietlenia listy i pobrania id wybranej organizacji */
            var chooseOragnizationDtoList = new List<ChooseOrganizationDto>();

            for (int i = 0; i < organizationNames.Count; i++)
            {
                ChooseOrganizationDto chooseOrganizationDto = new ChooseOrganizationDto()
                {
                    OrganizationId = organizationIds[i],
                    OrganizationName = organizationNames[i]
                };

                chooseOragnizationDtoList.Add(chooseOrganizationDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseOrganizations = chooseOragnizationDtoList;
            #endregion

            #region GettingUsersFromOrganization

            var organizationUsers = await _organizationUserServices.GetOrganizationUsers(organizationId);
            var organizationUsersNamesList = organizationUsers.Select(u => u.Name).ToList();
            var organizationUsersIdList = organizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = organizationUsersNamesList;

            #endregion

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationServices.GetById(organizationId);
            string chosenOranizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOranizationName;

            #endregion

            #region ChooseUserDtoLoading



            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < organizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = organizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);

        }

        [HttpPost]
        public async Task<IActionResult> InviteUser(int organizationId, string email)
        {
            var model = new ManageOrganizationDtoRefactor
            {
                SelectedOrganizationId = organizationId
            };

            #region UserIdGetter
            /* pobranie id użytkownika */
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);
            #endregion

            #region GettingOrganizationNamesAndIdsToDto
            /* pobranie organizacji użytkownia */
            var organizations = await _organizationUserServices.GetUserOrganizations(userId);

            /* wyselekcjonowanie nazw i id organizacji uzytkownia */
            List<string> organizationNames = organizations.Select(o => o.Name).ToList();
            List<int> organizationIds = organizations.Select(o => o.Id).ToList();

            /* przypisanie nazw i id do modelu */
            model.OrganizationNames = organizationNames;
            model.OrganizationIds = organizationIds;


            #endregion

            #region ChooseOrganizationDtoLoading
            /* zaladowanie do dto ChooseOrganizationDto nazw i id w celu wyswietlenia listy i pobrania id wybranej organizacji */
            var chooseOragnizationDtoList = new List<ChooseOrganizationDto>();

            for (int i = 0; i < organizationNames.Count; i++)
            {
                ChooseOrganizationDto chooseOrganizationDto = new ChooseOrganizationDto()
                {
                    OrganizationId = organizationIds[i],
                    OrganizationName = organizationNames[i]
                };

                chooseOragnizationDtoList.Add(chooseOrganizationDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseOrganizations = chooseOragnizationDtoList;
            #endregion

            #region GettingUsersFromOrganization

            var organizationUsers = await _organizationUserServices.GetOrganizationUsers(organizationId);
            var organizationUsersNamesList = organizationUsers.Select(u => u.Name).ToList();
            var organizationUsersIdList = organizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = organizationUsersNamesList;

            #endregion

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationServices.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion

           

            #region AddingUser


            try
            {
                var allUsersFromDatabase = await _userServices.GetAll();
                bool userExist = allUsersFromDatabase.Any(u => u.Email == email);

             

                if (!userExist)
                {
                    TempData["NoUsersMessage"] = $"User with email {email} does not exist in our base.";
                }
                else
                {
                    var newUser = allUsersFromDatabase.FirstOrDefault(u => u.Email == email);
                    int newUserId = newUser.Id;

                    if (await _organizationUserServices.IsUserSignedToOrganization(newUserId, organizationId))
                    {
                        TempData["userAlreadySignedMessage"] = $"User with email {email} is already signed to {chosenOrganizationName}.";
                    }
                    
                    bool addingSucceeded = await _organizationServices.AddUser(organizationId, newUserId);

                    if (addingSucceeded)
                    {
                        TempData["userAddedMessage"] = $"User with {email} was added to organization.";
                        var updatedOrganizationUsers = await _organizationUserServices.GetOrganizationUsers(organizationId);
                        var updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
                        model.OrganizationUserNames = organizationUsersNamesList;

                    }
                }
            }
            catch
            {
                return View("Manage", model);
            }


            #endregion

            #region ChooseUserDtoLoading



            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < organizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = organizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);
            
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromOrganization(int organizationId, int userToRemoveId)
        {
            var model = new ManageOrganizationDtoRefactor
            {
                SelectedOrganizationId = organizationId
            };

            #region UserIdGetter
            /* pobranie id użytkownika */
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);
            #endregion

            #region GettingOrganizationNamesAndIdsToDto
            /* pobranie organizacji użytkownia */
            var organizations = await _organizationUserServices.GetUserOrganizations(userId);

            /* wyselekcjonowanie nazw i id organizacji uzytkownia */
            List<string> organizationNames = organizations.Select(o => o.Name).ToList();
            List<int> organizationIds = organizations.Select(o => o.Id).ToList();

            /* przypisanie nazw i id do modelu */
            model.OrganizationNames = organizationNames;
            model.OrganizationIds = organizationIds;


            #endregion

            #region ChooseOrganizationDtoLoading
            /* zaladowanie do dto ChooseOrganizationDto nazw i id w celu wyswietlenia listy i pobrania id wybranej organizacji */
            var chooseOragnizationDtoList = new List<ChooseOrganizationDto>();

            for (int i = 0; i < organizationNames.Count; i++)
            {
                ChooseOrganizationDto chooseOrganizationDto = new ChooseOrganizationDto()
                {
                    OrganizationId = organizationIds[i],
                    OrganizationName = organizationNames[i]
                };

                chooseOragnizationDtoList.Add(chooseOrganizationDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseOrganizations = chooseOragnizationDtoList;
            #endregion

            #region GettingUsersFromOrganization

            var organizationUsers = await _organizationUserServices.GetOrganizationUsers(organizationId);
            var organizationUsersNamesList = organizationUsers.Select(u => u.Name).ToList();
            var organizationUsersIdList = organizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = organizationUsersNamesList;

            #endregion

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationServices.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion



            #region RemovingUser

            List<string> updatedOrganizationUsersNamesList = new();

            try
            {
               var organizationUserToRemove = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.UserId == userToRemoveId && ou.OrganizationId == organizationId);

               var userToBeRemovedFromOrganization = await _userServices.GetById(userToRemoveId);

                if (organizationUserToRemove.Role == Position.Manager || organizationUserToRemove.Role == Position.Owner)
                {
                    TempData["UserToRemoveIsAnOwnerOrManager"] = $"You cannot remove owner or manager.";
                }
                else
                {
                    if (await _organizationUserServices.RemoveUserFromOrganization(userToRemoveId, organizationId))
                    {
                        TempData["UserRemovedSuccessfully"] = $"User with email {userToBeRemovedFromOrganization.Name} was removed from {chosenOrganizationName}.";
                    }

                    
                }
            }
            catch
            {
                return View("Manage", model);
            }

            #endregion

            #region ChooseUserDtoLoading

            var updatedOrganizationUsers = await _organizationUserServices.GetOrganizationUsers(organizationId);
            updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;

            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < updatedOrganizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = organizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);

        }


    }
}
