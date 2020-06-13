using Microsoft.Extensions.Options;
using PriApi.Data;
using PriApi.Model;
using PriApi.Model.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Services
{
    public interface ICustomerServices
    {
        Task<Customer> SaveAsync(Customer data);
        Task<Customer> UpdateAsync(Customer data);
        Task<Customer> DeleteAsync(string code);
        Customer GetByCodeAsync(string code);

        ICollection<Customer> GetAllAsync(CustomerParams productParams);
    }

    public class CustomerServices : ICustomerServices
    {
        public Primavera _Primavera { get; set; }

        public CustomerServices(IOptions<Primavera> primavera)
        {
            _Primavera = primavera.Value;
        }
        public Task<Customer> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public ICollection<Customer> GetAllAsync(CustomerParams productParams)
        {
            try
            {
                string campos = @"
                    Cliente as Code, Nome as Name, Desconto as Discount,'' as [Type],
	                    isnull('PVP' + convert(nvarchar(10),(TipoPrec + 1)),'PVP1') [TypePrice] 
                ";

                string joins = "";
                string filtros = "";

                if (productParams.Code != null && productParams.Code.Length > 0)
                {
                    filtros = string.Format(" Cliente = '{0}'",
                        productParams.Code
                    );
                }

                if (productParams.Name != null && productParams.Code.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("Nome = '{0}' ", productParams.Name);
                }

                if (productParams.FullSearch != null && productParams.FullSearch.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("Cliente like '%{0}%' or Nome like '%{0}%' ", productParams.FullSearch);
                }

                if (productParams.Type != null && productParams.Type.Length > 0)
                {
                    //filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    //filtros += string.Format("TipoDoc = '{0}' ", productParams.Type);
                }
                
                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                DataTable dt = db.daListaTabela("Clientes", 500, campos, filtros, "", "cliente asc");

                List<Customer> customers = new List<Customer>();

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Customer()
                    {
                        Code = StringHelper.DaString(dr["Code"]),
                        Name = StringHelper.DaString(dr["Name"]),
                        Discount = StringHelper.DaDouble(dr["Discount"]),
                        TypePrice = StringHelper.DaString(dr["TypePrice"])
                    };

                    customers.Add(item);
                }

                return customers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer GetByCodeAsync(string code)
        {
            try
            {
                string campos = @"
                    Cliente as Code, Nome as Name, Desconto as Discount,'' as [Type],
	                    isnull('PVP' + convert(nvarchar(10),(TipoPrec + 1)),'PVP1') [TypePrice]
                ";

                string joins = "";
                string filtros = "";

                if (code != null && code.Length > 0)
                {
                    filtros = string.Format(" Cliente = '{0}'",
                        code
                    );
                }

                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                DataTable dt = db.daListaTabela("Clientes", 500, campos, filtros, "", "cliente asc");

                Customer customer = new Customer();

                foreach (DataRow dr in dt.Rows)
                {
                    customer = new Customer()
                    {
                        Code = StringHelper.DaString(dr["Code"]),
                        Name = StringHelper.DaString(dr["Name"]),
                        Discount = StringHelper.DaDouble(dr["Discount"]),
                        TypePrice = StringHelper.DaString(dr["TypePrice"])
                    };
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Customer> SaveAsync(Customer data)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateAsync(Customer data)
        {
            throw new NotImplementedException();
        }
    }
}
