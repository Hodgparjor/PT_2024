using Data.Database;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext
    {
        private const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\decyb\\OneDrive\\Pulpit\\Studia\\Sem6\\ProgrammingTechnologies\\PT_2024\\Task1\\Data\\Database\\DatabasePT.mdf;Integrated Security=True";
        internal List<Customer> customers = new();
        internal List<Supplier> suppliers = new();
        internal List<Event> events = new();
        internal List<Product> catalog = new();
        internal List<WarehouseEntry> warehouseState = new();
        private string connectionString;

        public DataContext(string? connectionString)
        {
            this.connectionString = connectionString ?? CONNECTION_STRING;
        }

        internal DataContext(List<Customer> customers, List<Supplier> suppliers, List<Event> events, List<Product> catalog, List<WarehouseEntry> warehouseState)
        {
            this.customers = customers;
            this.suppliers = suppliers;
            this.events = events;
            this.catalog = catalog;
            this.warehouseState = warehouseState;
        }

        public async Task AddCustomerAsync(IUser user)
        {
            using(PTLinq2SqlDataContext context = new(this.connectionString))
            {
                Database.User entity = new Database.User()
                {
                    id = user.Id,
                    name = user.Name
                };
                context.Users.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IUser?> GetCustomerAsync(int id)
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                Database.User? user = await Task.Run(() =>
                {
                    IQueryable<Database.User> query =
                        from u in context.Users
                        where u.id == id
                        select u;

                    return query.FirstOrDefault();
                });

                return user is not null ? new Customer(user.id, user.name) : null;
            }
        }

        public async Task UpdateCustomerAsync(IUser user)
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                Database.User toUpdate = (from u in context.Users where u.id == user.Id select u).FirstOrDefault()!;

                toUpdate.name = user.Name;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                Database.User toDelete = (from u in context.Users where u.id == id select u).FirstOrDefault()!;

                context.Users.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<int, IUser>> GetAllCustomersAsync()
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                IQueryable<IUser> usersQuery = from u in context.Users
                                               select
                                                   new Customer(u.id, u.name) as IUser;

                return await Task.Run(() => usersQuery.ToDictionary(k => k.Id));
            }
        }

        public async Task<int> GetCustomersCountAsync()
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                return await Task.Run(() => context.Users.Count());
            }
        }
    }
}
