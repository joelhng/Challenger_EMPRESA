using FteamLibrary.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace FteamApi.Api.Atributos
{
    public class AuthorizeLevel : AuthorizeAttribute, IAuthorizationFilter
    {
        public eTipoUsuario[] TipoUsuarios { get; set; }

        public AuthorizeLevel()
        {

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var identity = context.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

            eTipoUsuario nivel = eTipoUsuario.SinPrivilegios;

            if (identity != null)
            {
                Enum.TryParse(typeof(eTipoUsuario), identity.FindFirst("Nivel").Value, out object n);

                nivel = (eTipoUsuario)n;

            }

            foreach (var item in TipoUsuarios)
            {
                if (item == nivel)
                {
                    return;
                }
            }



            ////Validate if any permissions are passed when using attribute at controller or action level
            //if (string.IsNullOrEmpty(Permissions))
            //{
            //    //Validation cannot take place without any permissions so returning unauthorized
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}


            ////The below line can be used if you are reading permissions from token
            ////var permissionsFromToken=context.HttpContext.User.Claims.Where(x=>x.Type=="Permissions").Select(x=>x.Value).ToList()

            ////Identity.Name will have windows logged in user id, in case of Windows Authentication
            ////Indentity.Name will have user name passed from token, in case of JWT Authenntication and having claim type "ClaimTypes.Name"
            //var userName = context.HttpContext.User.Identity.Name;
            //var assignedPermissionsForUser = MockData.UserPermissions.Where(x => x.Key == userName).Select(x => x.Value).ToList();


            //var requiredPermissions = Permissions.Split(","); //Multiple permissiosn can be received from controller, delimiter "," is used to get individual values
            //foreach (var x in requiredPermissions)
            //{
            //    if (assignedPermissionsForUser.Contains(x))
            //        return; //User Authorized. Wihtout setting any result value and just returning is sufficent for authorizing user
            //}

            //context.Result = new UnauthorizedResult();

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
