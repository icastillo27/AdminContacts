using Blazor.Contacts.Wasm.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Repositories
{
    public interface IContactRepository
    {
        Task<bool> InsertConctact(Contact contact);

        Task<bool> UpdateContact(Contact contact);

        Task<int> DeleteContact(int id);

        Task<IEnumerable<Contact>> GetAll();

        Task<Contact> GetDetail(int id);
    }
}
