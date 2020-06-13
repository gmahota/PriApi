using Microsoft.Extensions.Options;
using PriApi.Data;
using PriApi.Helpers;
using PriApi.Model;
using PriApi.Model.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Services
{
    public interface IProductServices
    {
        Task<Product> SaveAsync(Product data);
        Task<Product> UpdateAsync(Product data);
        Task<Product> DeleteAsync(string code);
        Product GetByCodeAsync(string code);

        ICollection<Product> GetAllAsync(ProductParams productParams);
    }

    public class ProductServices : IProductServices
    {
        public Primavera _Primavera { get; set; }

        public ProductServices(IOptions<Primavera> primavera)
        {
            _Primavera = primavera.Value;
        }
        public Task<Product> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetAllAsync(ProductParams productParams)
        {
            try
            {
                string campos = @"
                    A.Artigo as Code, A.Descricao as [Description] , 
                        am.PVP1,am.PVP2,am.PVP3,am.PVP4,am.pvp5,am.PVP6
                ";

                string joins = "inner join ArtigoMoeda am on am.Artigo = a.Artigo and am.Moeda like 'M%' ";
                string filtros = "";

                if (productParams.Code != null && productParams.Code.Length > 0)
                {
                    filtros = string.Format(" A.Artigo = '{0}'",
                        productParams.Code
                    );
                }

                if (productParams.Description != null && productParams.Description.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("A.Descricao = '{0}' ", productParams.Description);
                }

                if (productParams.FullSearch != null && productParams.FullSearch.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("A.Artigo like '%{0}%' or A.Descricao like '%{0}%' ", productParams.FullSearch);
                }

                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                DataTable dt = db.daListaTabela("Artigo A", 0, campos, filtros,joins, "a.artigo asc");

                List<Product> products = new List<Product>();

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Product()
                    {
                        Code = StringHelper.DaString(dr["Code"]),
                        Description = StringHelper.DaString(dr["Description"]),
                        Pvp1 = StringHelper.DaDouble(dr["Pvp1"]),
                        Pvp2 = StringHelper.DaDouble(dr["Pvp2"]),
                        Pvp3 = StringHelper.DaDouble(dr["Pvp3"]),
                        Pvp4 = StringHelper.DaDouble(dr["Pvp4"]),
                        Pvp5 = StringHelper.DaDouble(dr["Pvp5"]),
                        Pvp6 = StringHelper.DaDouble(dr["Pvp6"])
                    };

                    products.Add(item);
                }

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product GetByCodeAsync(string code)
        {
            try
            {
                string campos = @"
                    select A.Artigo as Code, A.Descricao as [Description] , 
                        am.PVP1,am.PVP2,am.PVP3,am.PVP4,am.pvp5,am.PVP6
                ";

                string joins = "inner join ArtigoMoeda am on am.Artigo = a.Artigo and am.Moeda like 'M%' ";
                string filtros = "";

                if (code != null && code.Length > 0)
                {
                    filtros = string.Format(" A.Artigo = '{0}'",
                        code
                    );
                }

                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                DataTable dt = db.daListaTabela("Artigo A", 0, campos, filtros, joins, "a.artigo asc");

                Product product = new Product();

                foreach (DataRow dr in dt.Rows)
                {
                    product = new Product()
                    {
                        Code = StringHelper.DaString(dr["Code"]),
                        Description = StringHelper.DaString(dr["Description"]),
                        Pvp1 = StringHelper.DaDouble(dr["Pvp1"]),
                        Pvp2 = StringHelper.DaDouble(dr["Pvp2"]),
                        Pvp3 = StringHelper.DaDouble(dr["Pvp3"]),
                        Pvp4 = StringHelper.DaDouble(dr["Pvp4"]),
                        Pvp5 = StringHelper.DaDouble(dr["Pvp5"]),
                        Pvp6 = StringHelper.DaDouble(dr["Pvp6"])
                    };
                }

                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Product> SaveAsync(Product data)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
