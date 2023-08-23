using Microsoft.AspNetCore.Authorization;

namespace UserAPI.Authorization;

public class MinimunAge: IAuthorizationRequirement 
{
    public int Age { get; private set; }

    public MinimunAge(int age)
    {
        Age = age;
    }

}
