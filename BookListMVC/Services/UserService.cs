using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookListMVC.Services {
  public class UserService {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor) {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserCurrentUserId() {
      return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

  }
}
