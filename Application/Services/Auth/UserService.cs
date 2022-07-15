using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.Auth;
using Application.Interfaces.Services.General;
using AutoMapper;
using Domain.Common;
using Domain.Dto.General.Auth;
using Domain.Entites.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{

    public class UserService : ServiceBase, IUserService
    {
        private readonly IMapper _Mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager ;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IAttachmentRepository _attachmentRepository;


        //private readonly IEmailSender _emailSender;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAppUserRepository _appUserRepository;

        public UserService(
            SignInManager<ApplicationUser> signInManager,   
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork
            ,RoleManager<ApplicationRole> roleManager,
            IAppUserRepository appUserRepository,
            IFileService fileService,IAttachmentRepository attachmentRepository
            )
        {
            _Mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _fileService = fileService;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<ServiceResponse<TokenDto>> Token(LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                    return new ServiceResponse<TokenDto> { Success = false, Data = null, Message = "user name or password is Empty" };

                var token = await _appUserRepository.
                    GetToken(loginDto.Email, loginDto.Password, "SuperSecretPassword", "PetKeeper.com", "PetKeeper.com");
                if (token == null)
                    return new ServiceResponse<TokenDto> { Success = false, Data = null, Message = "Invaild Login" };
                //if(!token.IsActive)
                //    return new ServiceResponse<TokenDto> { Success = false, Data = null, Message = "you are not accepted by admin Yet" };

                var tokenModel = _Mapper.Map<TokenDto>(token);
                
                return new ServiceResponse<TokenDto> { Success = true, Data = tokenModel,Message="sign in succsessfully" };
            }
            catch (Exception ex)
            {
                return await LogError<TokenDto>(ex, null);
            }
        }


        public async Task<ServiceResponse<int>> RegisterAccounUser(RegisterDto registerAccountUserDto, bool status)
        {
            try
            {
                #region Guard
                var userExists = _appUserRepository.GetUserByEmail(registerAccountUserDto.Email);
                if (userExists != null) return new ServiceResponse<int> { Success = false, Message = "User is Already Exist" };
                #endregion
                var user = _Mapper.Map<ApplicationUser>(registerAccountUserDto);
                user.UserName = registerAccountUserDto.Email;
                if (status)
                {
                    user.Status = true;
                }
                var result = await _userManager.CreateAsync(user, registerAccountUserDto.Password);
                //await _fileService.UploadFile(user.Id, scondRowId: null, new List<IFormFile> { registerAccountUserDto.UserPic }, nameof(user), "000", "UsersPic", 500000);
                if (!result.Succeeded) return new ServiceResponse<int> { Success = false, Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)) };
                if (status == true)//client
                {
                    await _appUserRepository.AddRoleToUser(user, RolesName.Client);
                    
                }
                else
                {
                    await _appUserRepository.AddRoleToUser(user, RolesName.BusinessOwner);
                   
                }


                var res =await _unitOfWork.CommitAsync();
                return new ServiceResponse<int> { Success = true, Data = 1,Message="Your are Registered Succsfully" };
            }
            catch (Exception ex)
            {
                 return await LogError<int>(ex, 0);
            }
        }

  
        public async Task<ServiceResponse<int>> SigOutAsync()
        {
            try
            {
                var res = _signInManager.SignOutAsync();
                return new ServiceResponse<int> { Success = true, Data = 1, Message = "Your are Loged Out Succsfully" };


            }
            catch (Exception ex)
            {

                return await LogError<int>(ex, 0);
            }
        }
        public async Task<ServiceResponse<GetUserAccountDto>> GetUserAccount(Guid Id)
        {
            try
            {
                var user = _appUserRepository.GetUserById(Id);
                GetUserAccountDto getUserAccountDto = new GetUserAccountDto();
                getUserAccountDto.UserName = user.UserName;
                getUserAccountDto.FullName= user.FullName;
                getUserAccountDto.Email = user.Email;
                getUserAccountDto.PhoneNumber = user.PhoneNumber;
                var listofpic = await _attachmentRepository.GetAllAsync(p => p.Row_Id == Id.ToString());
                if (listofpic.Count != 0)
                {
                    var y =( await _attachmentRepository.GetAllAsync(p => p.Row_Id == Id.ToString())).FirstOrDefault().File_Path;
                    getUserAccountDto.UserPic = y;


                }

                return new ServiceResponse<GetUserAccountDto> { Success = true, Data = getUserAccountDto };


            }
            catch (Exception ex)
            {

                 throw ex;
            }
        }

        public async Task<ServiceResponse<int>> UpdateUser(Guid id, UserDto userDto)
        {
            try
            {
                var user = _appUserRepository.GetUserById(id);
                user.Id = id;
                user.FullName = userDto.FullName;
                user.PhoneNumber = userDto.PhoneNumber;
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                if(userDto.UserPic != null)
                {
                    var attach = _attachmentRepository.GetAll(a=>a.Row_Id == id.ToString()).FirstOrDefault();
                    if (attach != null&&attach.File_Name != userDto.UserPic.FileName)
                    {
                        _attachmentRepository.PhysiscalDelete(user.Id);
                    }
                    await _fileService.UploadFile(user.Id, null, new List<IFormFile> { userDto.UserPic }, nameof(user), "000", "UsersPic", 500000);

                }
                await _userManager.UpdateAsync(user);
                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int> { Success = true, Data = 1, Message = "Your Account Data Was updated" };
            }
            catch (Exception ex)
            {

                return await LogError<int>(ex, 0);
            }
        }


        public async Task<ServiceResponse<int>> DeletAccountUser(Guid Id)
        {
            try
            {
                var user = _appUserRepository.GetUserById(Id);
                 _appUserRepository.RemoveUser(user);
                return new ServiceResponse<int> { Success = true, Data = 1, Message="Your Account Deleted" };
            }
            catch (Exception ex)
            {
               return await LogError<int>(ex, 0);
            }
        }

        public async Task<ServiceResponse<int>> ChangePassword(Guid Id,string ?CurrentPass,String? NewPass)
        {
            try
            {
                if (CurrentPass ==null || NewPass == null) return new ServiceResponse<int> { Data = 0, Message = "Make Sure that you enter fields", Success = false };
                var user = _appUserRepository.GetUserById(Id);
                await _userManager.ChangePasswordAsync(user, CurrentPass, NewPass);
                await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Data = 1,
                    Success = true,
                    Message = "Your Password changed Successfully"
                };

            }
            catch (Exception ex)
            {
                return await LogError(ex, 0);
            }

        } 




        //private async Task<bool> CheckIfWorkUnitEmployeeActiveAsync(ApplicationUser user)
        //{
        //    var workUniteData = await _workUnitRepo.GetWorkUnitEmployeesById(user.Id);
        //    if (workUniteData == null) return false;
        //    var isActiveWorkUnit = (workUniteData.WorkUnitIsActiveFemale || workUniteData.WorkUnitIsActiveMale) && user.Is_Active;
        //    return isActiveWorkUnit;
        //}

        //public async Task<ServiceResponse<ApplicationUser>> RegisterAccounUser(AddAspNetUserDto registerAccountUserDto, List<string> Roles, bool isEstablishUser = false)
        //{
        //    try
        //    {
        //        var userExists = await _appUserRepo.GetUsersByName(registerAccountUserDto.UserName);
        //        if (userExists != null && userExists?.Count > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.IsExists)) };
        //        var mailExists = await _appUserRepo.GetUsersByMail(registerAccountUserDto.Email);
        //        if (mailExists != null && mailExists?.Count > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.EmailExists)) };
        //        if (!isEstablishUser)
        //        {
        //            var usersWithsameIdNumber = _appUserRepo.GetUsersByIdNumber(registerAccountUserDto.IdNumber);
        //            if (usersWithsameIdNumber != null && usersWithsameIdNumber.Count > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.IdNumberExists)) };
        //            var usersWithsamePhoneNumber = _appUserRepo.GetUsersByPhoneNumber(registerAccountUserDto.PhoneNumber);
        //            if (usersWithsamePhoneNumber != null && usersWithsamePhoneNumber.Count > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.PhoneNumberExists)) };
        //        }
        //        ApplicationUser user = ActivateUser(registerAccountUserDto);
        //        var result = await _userManager.CreateAsync(user, registerAccountUserDto.Password);
        //        if (!result.Succeeded) return new ServiceResponse<ApplicationUser> { Success = false, Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)) };
        //        if (Roles != null) { await _AppUserRepository.AddRolesToUser(user, Roles); }
        //        return new ServiceResponse<ApplicationUser> { Success = true, Data = user };
        //    }
        //    catch (Exception ex)
        //    {
        //        return await LogError<ApplicationUser>(ex, null);
        //    }
        //}

        //private ApplicationUser ActivateUser(AddAspNetUserDto registerAccountUserDto)
        //{
        //    ApplicationUser user = _Mapper.Map<ApplicationUser>(registerAccountUserDto);
        //    user.Is_Active = true;
        //    return user;
        //}



        //public async Task<ServiceResponse<ApplicationUser>> EditAccountUser(EditAspNetUserDto editAccountUserDto, List<string> Roles)
        //{
        //    try
        //    {
        //        var userExists = await _userManager.FindByNameAsync(editAccountUserDto.UserName);
        //        if (userExists != null)
        //            if (userExists.Id != editAccountUserDto.Id)
        //                return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.IsExists)) };
        //        var mailExists = _appUserRepo.GetUsersByEmailForEdit(editAccountUserDto.Email, editAccountUserDto.Id);
        //        if (mailExists.Count() > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.EmailExists)) };
        //        var phoneNumberofUser = _appUserRepo.GetUsersByPhoneNumberForEdit(editAccountUserDto.PhoneNumber, editAccountUserDto.Id);
        //        if (phoneNumberofUser.Count() > 0) return new ServiceResponse<ApplicationUser> { Success = false, Message = CultureHelper.GetResourceMessage(EmployeeResource.ResourceManager, nameof(EmployeeResource.PhoneNumberExists)) };
        //        var user = await _userManager.FindByIdAsync(editAccountUserDto.Id.ToString());
        //        await EditUserRoles(Roles, user);
        //        EditData(editAccountUserDto, user);
        //        var result = await _userManager.UpdateAsync(user);
        //        //if (!result.Succeeded) return new ServiceResponse<ApplicationUser> { Success = false, Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)) };
        //        return new ServiceResponse<ApplicationUser> { Success = true, Data = user };
        //    }
        //    catch (Exception ex)
        //    {
        //        return await LogError<ApplicationUser>(ex, null);
        //    }
        //}

        //private void EditData(EditAspNetUserDto editAccountUserDto, ApplicationUser user)
        //{

        //    user.Full_Name_Ar = editAccountUserDto.FullNameAr;
        //    user.Full_Name_En = editAccountUserDto.FullNameEn;
        //    user.UserName = editAccountUserDto.UserName;
        //    user.Email = editAccountUserDto.Email;
        //    user.Gender_Master_Code = editAccountUserDto.GenderMasterCode;
        //    user.PhoneNumber = editAccountUserDto.PhoneNumber;
        //    if (editAccountUserDto.Password != "" && editAccountUserDto.Password != null)
        //    {
        //        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editAccountUserDto.Password);
        //    }

        //}

        //private async Task EditUserRoles(List<string> Roles, ApplicationUser user)
        //{
        //    if (Roles != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        await _userManager.RemoveFromRolesAsync(user, userRoles);
        //        await _userManager.AddToRolesAsync(user, Roles);
        //    }
        //}

        //public async Task<ServiceResponse<ApplicationUser>> ChangeRolesToUserId(string userId, string Role)
        //{
        //    try
        //    {
        //        await _AppUserRepository.ChangeRolesToUserId(userId, Role);
        //        return new ServiceResponse<ApplicationUser> { Success = false, Message = "" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return await LogError<ApplicationUser>(ex, null);
        //    }
        //}

        //public async Task<ServiceResponse<Array>> Resetpassword(string Newpassword, string UserId)
        //{
        //    try
        //    {
        //        ApplicationUser userEntity = await _appUserRepo.GetUser(UserId);
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(userEntity);
        //        var result = await _userManager.ResetPasswordAsync(userEntity, token, Newpassword.Trim());

        //        if (!result.Succeeded)
        //        {
        //            return new ServiceResponse<Array>
        //            {
        //                Data = result.Errors.Where(er => er.Code.Contains("Password"))?.Select(er => er.Description).ToArray(),
        //                Message = CultureHelper.GetResourceMessage(CommonResource.ResourceManager, nameof(CommonResource.NotSaved)),
        //                Success = false
        //            };
        //        }
        //        else
        //        {
        //            return new ServiceResponse<Array>
        //            {
        //                Data = null,
        //                Message = GetCurrentLanguage() == Language.Arabic ? " تم التعديل بنجاح " : "Reset successfully",
        //                Success = true
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return await LogError<Array>(ex, null);

        //    }
        //}


        //public async Task<ServiceResponse<Array>> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        //{

        //    try
        //    {
        //        var userExists = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
        //        if (userExists != null)
        //        {
        //            string host = _httpContextAccessor.HttpContext.Request.Host.Value;


        //            var token = await _userManager.GeneratePasswordResetTokenAsync(userExists);
        //            var content = (await _contentDepartmentRepository.GetAllAsync(a => a.ContentDepartmentName == ContentDepartmentNamesCodes.ForgetPasswordEmail)).FirstOrDefault();

        //            var mailBoxAddress = new List<MimeKit.MailboxAddress> { new MimeKit.MailboxAddress(userExists.Full_Name_En, forgetPasswordDto.Email) };
        //            await _emailSender.SendEmailAsync(new EmailMessage
        //            {
        //                To = mailBoxAddress,
        //                Content = string.Format("<h3>تغيير كلمه السر:<h3/><p> لتجديد كلمه السر رجاء اضغط هنا</P><a class='btn btn-primary' href = '" + forgetPasswordDto.Url + "?token=" + token + "&email=" + forgetPasswordDto.Email + "' > اضغط هنا </a>"),
        //                Subject = "ForgetPassword"
        //            });
        //            return new ServiceResponse<Array>
        //            {
        //                Data = null,
        //                Message = GetCurrentLanguage() == Language.Arabic ? " تم ارسال لك رسالة تأكيد " : "A confirmation message has been sent to you",
        //                Success = true
        //            };
        //        }
        //        else
        //        {
        //            return new ServiceResponse<Array>
        //            {
        //                Data = null,
        //                Message = GetCurrentLanguage() == Language.Arabic ? " هذا البريد غير موجود من قبل" : "This mail does not exist before",
        //                Success = false
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ServiceResponse<Array>
        //        {
        //            Data = null,
        //            Message = ex.Message,
        //            Success = false
        //        };
        //    }
        //}
        //private static EmailMessage EmailContent(string userName, string emailAddress, string token, string url, ContentDepartment contentDepartment)
        //{
        //    return new EmailMessage()
        //    {
        //        Subject = (GetCurrentLanguage() == Language.Arabic.ToString()) ? "طلب انشاء حساب بادر" : "Bader Registration Request",
        //        Header = (GetCurrentLanguage() == Language.Arabic.ToString()) ? contentDepartment.Content_Header_Ar : contentDepartment.Content_Header_En,
        //        Content = (GetCurrentLanguage() == Language.Arabic.ToString()) ? contentDepartment.Content_Ar.Replace("{email}", emailAddress).Replace("{token}", token).Replace("{url}", url) : contentDepartment.Content_En.Replace("{email}", emailAddress).Replace("{token}", token).Replace("url", url),
        //        Footer = "شكرا بادر ",
        //        To = new List<MimeKit.MailboxAddress>()
        //        {
        //            new MimeKit.MailboxAddress(userName, emailAddress )
        //        }
        //    };
        //}

        //public async Task<ServiceResponse<Array>> ResetpasswordForAnonymousUser(RestPasswordForAnonymousUserDto RestPasswordForAnonymousUserDto)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(RestPasswordForAnonymousUserDto.Email);
        //        if (user != null)
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var result = await _userManager.ResetPasswordAsync(user, token, RestPasswordForAnonymousUserDto.Password.Trim());

        //            if (!result.Succeeded)
        //            {
        //                return new ServiceResponse<Array>
        //                {
        //                    Data = result.Errors.Where(er => er.Code.Contains("Password"))?.Select(er => er.Description).ToArray(),
        //                    Message = CultureHelper.GetResourceMessage(CommonResource.ResourceManager, nameof(CommonResource.NotSaved)),
        //                    Success = false
        //                };
        //            }
        //            else
        //            {
        //                return new ServiceResponse<Array>
        //                {
        //                    Data = null,
        //                    Message = GetCurrentLanguage() == Language.Arabic ? " تم التعديل بنجاح " : "Reset successfully",
        //                    Success = true
        //                };
        //            }
        //        }
        //        else
        //        {

        //            return new ServiceResponse<Array>
        //            {
        //                Data = null,
        //                Message = CultureHelper.GetResourceMessage(CommonResource.ResourceManager, nameof(CommonResource.NotSaved)),
        //                Success = false
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return await LogError<Array>(ex, null);

        //    }
        //}
    }

}

