using OnSale.Common.Entities;
using OnSale.Common.Enums;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Rafael", "Santos", "rafaasfs@gmail.com", "322 311 4620", "Rua do Sol e da Lua", UserType.Admin);

        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task<User> CheckUserAsync(
        string document,
        string firstName,
        string lastName,
        string email,
        string phone,
        string address,
        UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }


        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Portugal",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Bragança",
                        Cities = new List<City>
                        {
                            new City { Name = "Vinhais" },
                            new City { Name = "Mogadouro" },
                            new City { Name = "Mirandela" }
                        }
                    },
                    new Department
                    {
                        Name = "Lisboa",
                        Cities = new List<City>
                        {
                            new City { Name = "Loures" }
                        }
                    },
                    new Department
                    {
                        Name = "Santarém",
                        Cities = new List<City>
                        {
                            new City { Name = "Cartaxo" },
                            new City { Name = "Almeirim" },
                            new City { Name = "Coruche" }
                        }
                    }
                }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos da América",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "California",
                        Cities = new List<City>
                        {
                            new City { Name = "Los Angeles" },
                            new City { Name = "San Diego" },
                            new City { Name = "San Francisco" }
                        }
                    },
                    new Department
                    {
                        Name = "Illinois",
                        Cities = new List<City>
                        {
                            new City { Name = "Chicago" },
                            new City { Name = "Springfield" }
                        }
                    }
                }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
