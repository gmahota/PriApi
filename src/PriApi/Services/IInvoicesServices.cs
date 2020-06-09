using PriApi.Model;
using PriApi.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Services
{
    public interface IInvoicesServices
    {
        Task<Invoice> SaveAsync(Invoice data);
        Task<Invoice> UpdateAsync(Invoice data);
        Task<Invoice> DeleteAsync(long data);
        Task<Invoice> GetByIdAsync(long id);

        ICollection<Invoice> GetAllAsync(InvoiceParams productParams);

    }
}
