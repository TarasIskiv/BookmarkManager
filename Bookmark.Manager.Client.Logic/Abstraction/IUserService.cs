using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Client.Logic.Abstraction
{
    public interface IUserService
    {
        Task Login(UserLoginPayload userLogin);
        Task SignUp(UserSignUpPayload userSignUp);
        Task Logout();
    }
}