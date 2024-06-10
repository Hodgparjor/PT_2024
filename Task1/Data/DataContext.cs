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
        //private const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\decyb\\OneDrive\\Pulpit\\Studia\\Sem6\\ProgrammingTechnologies\\PT_2024\\Task1\\Data\\Database\\DatabasePT.mdf;Integrated Security=True";
        internal List<Customer> customers = new();
        internal List<Supplier> suppliers = new();
        internal List<Event> events = new();
        internal List<Product> catalog = new();
        internal List<WarehouseEntry> warehouseState = new();
        private string connectionString;
        PTLinq2SqlDataContext context;


        public DataContext(string? connectionString = null)
        {
            if(connectionString is null)
            {
                string projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName;
                string databaseRelativePath = @"Data\Database\DatabasePT.mdf";
                string databasePath = Path.Combine(projectRootDir, databaseRelativePath);
                this.connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databasePath};Integrated Security = True; Connect Timeout = 30;";
            }
            else
            {
                this.connectionString = connectionString;
            }
            //this.connectionString = CONNECTION_STRING;
            context = new PTLinq2SqlDataContext(this.connectionString);
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
                Database.User entity = new Database.User()
                {
                    id = user.Id,
                    name = user.Name
                };
                context.Users.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<IUser?> GetCustomerAsync(int id)
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

        public async Task UpdateCustomerAsync(IUser user)
        {
                Database.User toUpdate = (from u in context.Users where u.id == user.Id select u).FirstOrDefault()!;

                toUpdate.name = user.Name;

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task DeleteCustomerAsync(int id)
        {
                Database.User toDelete = (from u in context.Users where u.id == id select u).FirstOrDefault()!;

                context.Users.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<Dictionary<int, ICustomer>> GetAllCustomersAsync()
        {
                IQueryable<ICustomer> usersQuery = from u in context.Users
                                               select
                                                   new Customer(u.id, u.name) as ICustomer;

                return await Task.Run(() => usersQuery.ToDictionary(k => k.Id));
        }

        public async Task<int> GetCustomersCountAsync()
        {
            using (PTLinq2SqlDataContext context = new PTLinq2SqlDataContext(this.connectionString))
            {
                return await Task.Run(() => context.Users.Count());
            }
        }

        public async Task<bool> DoesCustomerExist(int id)
        {
            return (await GetCustomerAsync(id)) != null;
        }

        #region Product CRUD

        public async Task AddProductAsync(IProduct product)
        {
                Database.Product entity = new Database.Product()
                {
                    id = product.Id,
                    name = product.Name,
                    price = (double)product.Price,
                };
                context.Products.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<IProduct?> GetProductAsync(int id)
        {
            Database.Product? product = await Task.Run(() =>
            {
                return context.Products
                            .Where(p => p.id == id)
                            .FirstOrDefault();
            });
            return product is not null ? new Product(product.id, product.name, (decimal)product.price) : null;
        }

        public async Task UpdateProductAsync(IProduct product)
        {
            Database.Product toUpdate = context.Products
                                            .Where(p => p.id == product.Id)
                                            .FirstOrDefault()!;

            toUpdate.name = product.Name;
            toUpdate.price = (double)product.Price;

            await Task.Run(() => context.SubmitChanges());
        }

        public async Task DeleteProductAsync(int id)
        {
                Database.Product toDelete = context.Products
                                            .Where(p => p.id == id)
                                            .FirstOrDefault()!;

            context.Products.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<Dictionary<int, IProduct>> GetAllProductsAsync()
        {
            IQueryable<IProduct> productQuery = context.Products
                .Select(p => new Product(p.id, p.name, (decimal)p.price) as IProduct);

            return await Task.Run(() => productQuery.ToDictionary(k => k.Id));

        }

        public async Task<int> GetProductsCountAsync()
        {
                return await Task.Run(() => context.Products.Count());
        }

        public async Task<bool> DoesProductExists(int id)
        {
            return (await this.GetProductAsync(id)) != null;
        }

        #endregion

        #region State CRUD

        public async Task AddWarehouseEntryAsync(IWarehouseEntry state)
        {
                Database.WarehouseEntry entity = new Database.WarehouseEntry()
                {
                    id = state.Id,
                    productId = state.Product.Id,
                    quantity = state.Quantity
                };

                context.WarehouseEntries.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<IWarehouseEntry?> GetWarehouseEntryAsync(int id)
        {
                Database.WarehouseEntry? warehouseEntry = await Task.Run(() =>
                {
                    IQueryable<Database.WarehouseEntry> query =
                        from s in context.WarehouseEntries
                        where s.id == id
                        select s;

                    return query.FirstOrDefault();
                });

                return warehouseEntry is not null ? new WarehouseEntry(warehouseEntry.id, warehouseEntry.productId, warehouseEntry.quantity) : null;
        }

        public async Task UpdateWarehouseEntryAsync(IWarehouseEntry warehouseEntry)
        {
                Database.WarehouseEntry toUpdate = (from s in context.WarehouseEntries where s.id == warehouseEntry.Id select s).FirstOrDefault()!;

                toUpdate.productId = warehouseEntry.ProductId;
                toUpdate.quantity = warehouseEntry.Quantity;

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task DeleteWarehouseEntryAsync(int id)
        {
                Database.WarehouseEntry toDelete = (from s in context.WarehouseEntries where s.id == id select s).FirstOrDefault()!;

                context.WarehouseEntries.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync()
        {
                IQueryable<IWarehouseEntry> stateQuery = from s in context.WarehouseEntries
                                                select
                                                    new WarehouseEntry(s.id, s.productId, s.quantity) as IWarehouseEntry;

                return await Task.Run(() => stateQuery.ToDictionary(k => k.Id));
        }

        public async Task<int> GetWarehouseEntriesCountAsync()
        {
                return await Task.Run(() => context.WarehouseEntries.Count());
        }

        public async Task<bool> DoesWarehouseEntryExistsAsync(int id)
        {
            return (await this.GetWarehouseEntryAsync(id)) != null;
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(IEventSold even)
        {
                Database.Event entity = new Database.Event()
                {
                    id = even.Id,
                    stateId = even.WarehouseEntryId,
                    userId = even.CustomerId,
                    date = even.Date,
                    quantity = even.Quantity
                };

                context.Events.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<IEventSold?> GetEventAsync(int id)
        {
                Database.Event? even = await Task.Run(() =>
                {
                    IQueryable<Database.Event> query =
                        from e in context.Events
                        where e.id == id
                        select e;

                    return query.FirstOrDefault();
                });
                return even is not null ? new EventSold(even.id, even.stateId, even.userId, even.date, even.quantity) : null;
        }

        public async Task UpdateEventAsync(IEventSold even)
        {
                Database.Event toUpdate = (from e in context.Events where e.id == even.Id select e).FirstOrDefault()!;

                toUpdate.stateId = even.WarehouseEntryId;
                toUpdate.userId = even.CustomerId;
                toUpdate.date = even.Date;
                toUpdate.quantity = even.Quantity;

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task DeleteEventAsync(int id)
        {
                Database.Event toDelete = (from e in context.Events where e.id == id select e).FirstOrDefault()!;

                context.Events.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
        }

        public async Task<Dictionary<int, IEventSold>> GetAllEventsAsync()
        {
                IQueryable<IEventSold> eventQuery = from e in context.Events
                                                select
                                                    new EventSold(e.id, e.stateId, e.userId, e.date, e.quantity) as IEventSold;

                return await Task.Run(() => eventQuery.ToDictionary(k => k.Id));
        }

        public async Task<int> GetEventsCountAsync()
        {
                return await Task.Run(() => context.Events.Count());
        }

        public async Task<bool> DoesEventExists(int id)
        {
            return (await this.GetEventAsync(id)) != null;
        }

        #endregion
    }
}
