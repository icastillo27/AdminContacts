using Blazor.Contacts.Wasm.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task<int> DeleteContact(int id)
        {
            try
            {
                var sql = "DELETE FROM Contacts WHERE Id = @Id";

                return await _dbConnection.ExecuteAsync(sql, new { Id = id });

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            try
            {
                var sql = "SELECT Id, FirstName, LastName, Phone, Address " +
                    "FROM Contacts";

                return await _dbConnection.QueryAsync<Contact>(sql);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Contact> GetDetail(int id)
        {
            try
            {
                var sql = "SELECT Id, FirstName, LastName, Phone, Address " +
                    "FROM Contacts WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id });

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertConctact(Contact contact)
        {
            try
            {
                var sql = "INSERT INTO Contacts (FirstName, LastName, Phone, Address)" +
                           "VALUES (@FirstName, @LastName, @Phone, @Address)";
                var result = await _dbConnection.ExecuteAsync(sql,
                    new
                    {
                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address
                    });
                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            try
            {
                var sql = "UPDATE Contacts SET " +
                                                "FirstName = @FirstName," +
                                                "LastName = @LastName, " +
                                                "Phone = @Phone," +
                                                "Address = @Address " +
                                                "WHERE id = @Id ";
                var result = await _dbConnection.ExecuteAsync(sql,
                    new
                    {
                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address,
                        contact.Id
                    });
                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
