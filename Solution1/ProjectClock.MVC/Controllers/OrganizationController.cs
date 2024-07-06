using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.OrganizationUserServices;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.Database;
using ProjectClock.MVC.Extensions;
using Position = ProjectClock.Database.Entities.Position;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationService _organizationService;
        private IUserServices _userService;
        private IAccountServices _accountService;
        private IOrganizationUserService _organizationUserService;
        private ProjectClockDbContext _projectClockDbContext;
        private IMapper _mapper;
        private readonly IStringLocalizer<OrganizationController> _localizer;

        public OrganizationController(IOrganizationService organizationServices,
            IUserServices userServices,
            IAccountServices accountService,
            IOrganizationUserService organizationUserServices,
            ProjectClockDbContext projectClockDbContext,
            IMapper mapper,
            IStringLocalizer<OrganizationController> localizer)
        {
            _mapper = mapper;
            _userService = userServices;
            _organizationService = organizationServices;
            _accountService = accountService;
            _organizationUserService = organizationUserServices;
            _projectClockDbContext = projectClockDbContext;
            _localizer = localizer;
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
                    TempData["ErrorMessage"] = _localizer["CreateError"].Value;
                    return View();
                }


                HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
                organizationDto.UserId = await _accountService.GetUserIdFromAccountId(accountId);

                if (_organizationUserService.IsUserAnOwner(organizationDto.UserId))
                {
                    TempData["ErrorMessage"] = "You are already an owner of organization. You can only be owner of one organization.";
                }
                else
                {
                    bool created = await _organizationService.Create(organizationDto);

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

            var userOrganizations = await _organizationUserService.GetUserOrganizations(userId);

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
                bool deleted = await _organizationService.Delete(organizationId);

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
            ManageOrganizationDto model = new ManageOrganizationDto();

            #region UserIdGetter
            /* pobranie id użytkownika */
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);
            #endregion


            #region GettingOrganizationNamesAndIdsToDto
            /* pobranie organizacji użytkownika */
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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
            var model = new ManageOrganizationDto
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
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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

            var organizationUsers = await _organizationUserService.GetOrganizationUsers(organizationId);
            var organizationUsersNamesList = organizationUsers.Select(u => u.Name).ToList();
            var organizationUsersIdList = organizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = organizationUsersNamesList;
            

            #endregion

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationService.GetById(organizationId);
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
            var model = new ManageOrganizationDto
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
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationService.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion



            #region AddingUser

            try
            {
                var allUsersFromDatabase = await _userService.GetAll();
                bool userExist = allUsersFromDatabase.Any(u => u.Email == email);
              
                if (!userExist)
                {
                    TempData["NoUsersMessage"] = $"User with email {email} does not exist in our base.";
                }
                else
                {
                    var newUser = allUsersFromDatabase.FirstOrDefault(u => u.Email == email);
                    int newUserId = newUser.Id;

                    if (await _organizationUserService.IsUserSignedToOrganization(newUserId, organizationId))
                    {
                        TempData["userAlreadySignedMessage"] = $"User with email {email} is already signed to {chosenOrganizationName}.";
                    }
                    
                    bool addingSucceeded = await _organizationService.AddUser(organizationId, newUserId);

                    if (addingSucceeded)
                    {
                        TempData["userAddedMessage"] = $"Invitation was sent to user with email {email}.";
                    }
                }
            }
            catch
            {
                return View("Manage", model);
            }



            #endregion
            #region ChooseUserDtoLoading
           
            var updatedOrganizationUsers = await _organizationUserService.GetOrganizationUsers(organizationId);
            List<string> updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;
            var organizationUsersIdList = updatedOrganizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;

            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < updatedOrganizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = updatedOrganizationUsersNamesList[i]
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
            var model = new ManageOrganizationDto
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
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationService.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion

            #region RemovingUser

            try
            {
               var organizationUserToRemove = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.UserId == userToRemoveId && ou.OrganizationId == organizationId);

               var userToBeRemovedFromOrganization = await _userService.GetById(userToRemoveId);

                if (organizationUserToRemove.Role == Position.Manager || organizationUserToRemove.Role == Position.Owner)
                {
                    TempData["UserToRemoveIsAnOwnerOrManager"] = $"You cannot remove owner or manager.";
                }
                else
                {
                    if (await _organizationUserService.RemoveUserFromOrganization(userToRemoveId, organizationId))
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

            var updatedOrganizationUsers = await _organizationUserService.GetOrganizationUsers(organizationId);
            List<string> updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;
            var organizationUsersIdList = updatedOrganizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;

            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < updatedOrganizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = updatedOrganizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);

        }

        [HttpPost]
        public async Task<IActionResult> AssignManagerStatus(int organizationId, int userToBecomeManagerId)
        {
            var model = new ManageOrganizationDto
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
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationService.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion

            #region AssigningUserToManager

            try
            {
                var organizationUserToAdvance = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                     ou.UserId == userToBecomeManagerId && ou.OrganizationId == organizationId);

                var userToBeAdvanced = await _userService.GetById(userToBecomeManagerId);

                if (organizationUserToAdvance.Role == Position.Manager || organizationUserToAdvance.Role == Position.Owner)
                {
                    TempData["UserToAdvanceIsManagerOrOwner"] = $"You cannot advance owner or manager.";
                }//dodać warunek że tylko user moze dodawać promocje
                else
                {
                    if (await _organizationUserService.AdvanceUserToManager(userToBecomeManagerId, organizationId))
                    {
                        TempData["UserAdvancedSuccessfully"] = $"User {userToBeAdvanced.Name} was advanced to manager role.";
                    }
                }
            }
            catch
            {
                return View("Manage", model);
            }

            #endregion

            #region ChooseUserDtoLoading

            var updatedOrganizationUsers = await _organizationUserService.GetOrganizationUsers(organizationId);
            List<string> updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;
            var organizationUsersIdList = updatedOrganizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;

            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < updatedOrganizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = updatedOrganizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);

        }

        [HttpPost]
        public async Task<IActionResult> DegregadeFromManagerStatus(int organizationId, int userToDegradeId)
        {
            var model = new ManageOrganizationDto
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
            var organizations = await _organizationUserService.GetUserOrganizations(userId);

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

            #region SettingChosenOrganizationName

            var chosenOrganization = await _organizationService.GetById(organizationId);
            string chosenOrganizationName = chosenOrganization.Name;
            model.OrganizationName = chosenOrganizationName;

            #endregion

            #region DegregadeUserToManager

            try
            {
                var organizationUserToDegrade = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                     ou.UserId == userToDegradeId && ou.OrganizationId == organizationId);

                var managerToDegrade = await _userService.GetById(userToDegradeId);

                if (organizationUserToDegrade.Role == Position.User || organizationUserToDegrade.Role == Position.Owner)
                {
                    TempData["UserDegradeFromManagerFailedMessage"] = $"You cannot degrade this person, because it is owner or common user.";
                }
                else if (!await _organizationUserService.IsUserAnOwnerOfParticularOrganization(userId, organizationId))
                {
                    TempData["LoggedInUserIsNotAnOwner"] = $"You do not have right to degrade managers.";
                }
                else
                {
                    if (await _organizationUserService.DegradeManager(userToDegradeId, organizationId))
                    {
                        TempData["UserAdvancedSuccessfully"] = $"User {managerToDegrade.Name} was degraded from manager role.";
                    }
                }
            }
            catch
            {
                return View("Manage", model);
            }

            #endregion

            #region ChooseUserDtoLoading

            var updatedOrganizationUsers = await _organizationUserService.GetOrganizationUsers(organizationId);
            List<string> updatedOrganizationUsersNamesList = updatedOrganizationUsers.Select(u => u.Name).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;
            var organizationUsersIdList = updatedOrganizationUsers.Select(u => u.Id).ToList();
            model.OrganizationUserNames = updatedOrganizationUsersNamesList;

            var chooseUserDtoList = new List<ChooseUserDto>();

            for (int i = 0; i < updatedOrganizationUsersNamesList.Count; i++)
            {
                ChooseUserDto chooseUserDto = new ChooseUserDto()
                {
                    Id = organizationUsersIdList[i],
                    Name = updatedOrganizationUsersNamesList[i]
                };

                chooseUserDtoList.Add(chooseUserDto);
            }

            /* przypisanie do modelu dto */
            model.ChooseUserDto = chooseUserDtoList;
            #endregion

            return View("Manage", model);

        }

        // GET: OrganizationController/Delete/5
        public async Task<IActionResult> Invitation()
        {
            InvitationToOrganizationDto model = new InvitationToOrganizationDto();

            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);

            var invitingOrganizations = await _organizationUserService.GetInvitingOrganizations(userId);

            var invitingOrganizationsDtoList = invitingOrganizations.Select(x => new OrganizationDto()
            {
                OrganizationId = x.Id,
                OrganizationName = x.Name

            }).ToList();

            model.InvitingOrganizations = invitingOrganizationsDtoList;

            return View("Invitation", model);
        }

        // POST: OrganizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invitation(int organizationId)
        {


            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);

            try
            {//tu zrobić nową metodę w serwisie i poprawić metody do wyszukiwania organizacji z AcceptedInvitations, jak remove from organization to usun cały wiersz, jak create OU to z ustawieniem Invited na true or false?
                bool acceptedInvitation = await _organizationUserService.AcceptInvitation(userId, organizationId);
                var organization = await _organizationService.GetById(organizationId);
                var organizationName = organization.Name;

                if (acceptedInvitation)
                {
                    TempData["AcceptanceSuccessMessage"] = $"Invitation confirmed. Welcome to {organizationName}";
                }
                else
                {
                    TempData["AcceptanceErrorMessage"] = "There's problem with this invitation.";
                }

                return RedirectToAction(nameof(Invitation));
            }
            catch
            {
                return View();
            }
        }

    }
}
