﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Authentication;

namespace CPanel
{
    public class CPanelCKFinderAuthenticator : IAuthenticator
    {
        public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
        {
            var claimsPrincipal = commandRequest.Principal as ClaimsPrincipal;
            var isAuthenticated = false;
            if (claimsPrincipal != null)
            {
                var roles = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();

                /*
                 * Enable CKFinder only for authenticated users.
                 */
                //var isAuthenticated = claimsPrincipal.Identity.IsAuthenticated;

                isAuthenticated = true;

                var user = new User(isAuthenticated, roles);
                return Task.FromResult((IUser)user);
            }
            return null;
        }
    }
}