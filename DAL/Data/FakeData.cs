using Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Data;

public static class FakeData
{
    public static ICollection<User> Users = new List<User>
    {
        new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@aaa.com",
            NormalizedEmail = "admin@aaa.com".ToUpper(),
            UserName = "Sanya",
            NormalizedUserName = "Sanya".ToUpper(),
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
        },
        new User
        {
            Id = Guid.NewGuid(),
            Email = "user@aaa.com",
            NormalizedEmail = "user@aaa.com".ToUpper(),
            UserName = "Egor",
            NormalizedUserName = "Egor".ToUpper(),
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
        },
    };


    public static ICollection<IdentityRole<Guid>> Roles = new List<IdentityRole<Guid>>
    {
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "admin",
            NormalizedName = "admin"
        },
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "user",
            NormalizedName = "user"
        }
    };

    public static ICollection<IdentityUserRole<Guid>> UserRoles = new List<IdentityUserRole<Guid>>
    {
        new IdentityUserRole<Guid>
        {
            UserId = Users!.First().Id,
            RoleId = Roles.First().Id
        },
        new IdentityUserRole<Guid>
        {
            UserId = Users!.Last().Id,
            RoleId = Roles.Last().Id
        }
    };

    public static ICollection<Medicine> Medicines = new List<Medicine>
    {
        new Medicine
        {
            Id = Guid.NewGuid(),
            Name = "Paracetomol",
            Type = Entities.Enums.MedicineType.Pills,
            Price = 13,
            ByRecipe = false
        },
        new Medicine
        {
            Id = Guid.NewGuid(),
            Name = "Vishnevskiy",
            Type = Entities.Enums.MedicineType.Ointment,
            Price = 199.99m,
            ByRecipe = true
        },
        new Medicine
        {
            Id = Guid.NewGuid(),
            Name = "Kameton",
            Type = Entities.Enums.MedicineType.Aerosol,
            Price = 9.99m,
            ByRecipe = false
        },
        new Medicine
        {
            Id = Guid.NewGuid(),
            Name = "Antiflue",
            Type = Entities.Enums.MedicineType.Powder,
            Price = 17,
            ByRecipe = false
        },
        new Medicine
        {
            Id = Guid.NewGuid(),
            Name = "Meligen",
            Type = Entities.Enums.MedicineType.Mixture,
            Price = 19.95m,
            ByRecipe = false
        }
    };

    public static ICollection<Order> Orders = new List<Order>
    {
        new Order
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            DealCode = Guid.NewGuid().ToString(),
            Medicines = Medicines.Skip(0).Take(2).ToList(),
            UserId = Users?.FirstOrDefault()?.Id
        },
        new Order
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            DealCode = Guid.NewGuid().ToString(),
            Medicines = Medicines.Skip(2).Take(3).ToList(),
            UserId = Users?.LastOrDefault()?.Id
        }
    };
}