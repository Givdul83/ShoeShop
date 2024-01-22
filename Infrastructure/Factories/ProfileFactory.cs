
using Infrastructure.Entities;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Infrastructure.Factories;

public static class ProfileFactory
{
    public static ProfileEntity CreateProfileEntity(string firstName, string lastName)
    {
        try
        {
            var profileEntity = new ProfileEntity { FirstName = firstName, LastName = lastName};
            return profileEntity;
        }

        catch (Exception ex)
        {
            {
                Debug.WriteLine("ERROR :: CreateProfile" + ex.Message);
            }
            return null!;
        }
    }
}
