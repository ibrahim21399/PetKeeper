using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Services.Auth;
using AutoMapper;
using Domain.Common;
using Domain.Dto.General.Auth;
using Domain.Entites.General;
using Microsoft.AspNetCore.Identity;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{

    public class UserService : ServiceBase, IUserService
    {
        private readonly IMapper _Mapper;
            private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmailSender _emailSender;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAppUserRepository _appUserRepository;

        public UserService(

            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork
            ,RoleManager<ApplicationRole> roleManager,
            IAppUserRepository appUserRepository
            )
        {
            _Mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _appUserRepository = appUserRepository;
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
                //if (registerAccountUserDto.Role == RolesName.BusinessOwner.ToString())
                //{
                //    registerAccountUserDto.IsActive = false;
                //}
                //else
                //if (status== true)
                //{
                //    registerAccountUserDto.IsActive = true;
                //}else
                //    registerAccountUserDto.IsActive = user.IsActive = false;
                    

             
                     
                        

                var result = await _userManager.CreateAsync(user, registerAccountUserDto.Password);
                if (!result.Succeeded) return new ServiceResponse<int> { Success = false, Message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)) };
                //await _appUserRepository.AddRoleToUser(user, registerAccountUserDto.Role);
                if (status == true)//client
                {
                    await _appUserRepository.AddRoleToUser(user, RolesName.Client.ToString());
                    
                }
                else
                {
                    await _appUserRepository.AddRoleToUser(user, RolesName.BusinessOwner.ToString());
                   
                }


                var res =await _unitOfWork.CommitAsync();
                return new ServiceResponse<int> { Success = true, Data = 1,Message="Your are Registered Succsfully" };
            }
            catch (Exception ex)
            {
                 return await LogError<int>(ex, 0);
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


        //public async Task DeletAccountUser(string userName)
        //{
        //    try
        //    {
        //        await _AppUserRepository.DeleteUser(userName);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
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

