using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserAPI.Authorization
{
    public class AgeAuthorization : AuthorizationHandler<MinimunAge>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimunAge minimunAge)
        {
            var birthDateClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);
            if (birthDateClaim == null) return Task.CompletedTask;

            var birthDate = Convert.ToDateTime(birthDateClaim.Value);
            var years = DateTime.Now.Year - birthDate.Year;

            if (birthDate > DateTime.Today.AddYears(-years))
                years--;

            if (years >= minimunAge.Age) context.Succeed(minimunAge);
            else 
                context.Fail(new AuthorizationFailureReason(this, "Does not have minimun age."));
            return Task.CompletedTask;
        }
    }
}
