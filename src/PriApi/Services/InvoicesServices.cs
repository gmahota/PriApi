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
    public class InvoicesServices : IInvoicesServices
    {
        public Primavera _Primavera { get; set; }

        public InvoicesServices( IOptions<Primavera> primavera
            )
        {
            _Primavera = primavera.Value;
        }
        public Task<Invoice> DeleteAsync(long data)
        {
            throw new NotImplementedException();
        }

        public ICollection<Invoice> GetAllAsync(InvoiceParams productParams)
        {
            try
            {
                string campos = @"Id, entidade as Customer,Nome as Name ,documento as Document,
                Data as Date";

                string joins = "";
                string filtros = "";                

                if(productParams.DateBegin != null && productParams.DateEnd != null)
                {
                    filtros = string.Format(" Data between '{0}' and '{1}'",
                        productParams.DateBegin.ToString("yyyy-MM-dd"),
                        productParams.DateEnd.ToString("yyyy-MM-dd")
                    );
                }

                if (productParams.Document != null && productParams.Document.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("Documento = '{0}' ", productParams.Document);
                }

                if (productParams.Entity != null && productParams.Entity.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("Entidade = '{0}' ", productParams.Entity);
                }

                if (productParams.Type != null && productParams.Type.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("TipoDoc = '{0}' ", productParams.Type);
                }

                if (productParams.Reference != null && productParams.Reference.Length > 0)
                {
                    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    filtros += string.Format("Referencia = '{0}' or Requisicao ='{0}' ", 
                        productParams.Reference);
                }

                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                DataTable dt = db.daListaTabela("CabecDoc", 500, campos, filtros, "", "data desc");

                List<Invoice> invoices = new List<Invoice>();

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Invoice()
                    {
                        Id = StringHelper.DaString(dr["Id"]),
                        Customer = StringHelper.DaString(dr["Customer"]),
                        Document = StringHelper.DaString(dr["Document"]),
                        Date = Convert.ToDateTime(dr["Date"]),
                        _Customer =
                            new Customer
                            {
                                Code = StringHelper.DaString(dr["Customer"]),
                                Name = StringHelper.DaString(dr["Name"])
                            }
                    };

                    campos = @"Id,A.Artigo as Product, A.Descricao as 'Product_Desc',Ld.Descricao as 'Description', ld.Quantidade as 'Qty',
                        precUnit as PriceUnit, Desconto as Discount,TotalIliquido as Total, TaxaIva as Vat";

                    joins = "left outer join Artigo a on a.Artigo = ld.Artigo";

                    DataTable dtLinhas = 
                        db.daListaTabela("linhasdoc ld", 0, campos, 
                            string.Format("IdCabecDoc = '{0}'",item.Id), joins);

                    foreach(DataRow drLinhas in dtLinhas.Rows)
                    {
                        var line = new Invoice_Itens()
                        {
                            Id = StringHelper.DaString(drLinhas["Id"]),
                            Description = StringHelper.DaString(drLinhas["Description"]),
                            Qty = StringHelper.DaDouble(drLinhas["Qty"]),
                            PriceUnit = StringHelper.DaDouble(drLinhas["PriceUnit"]),
                            Discount = StringHelper.DaDouble(drLinhas["Discount"]),
                            Product = StringHelper.DaString(drLinhas["Product"]),
                            Vat = StringHelper.DaDouble(drLinhas["Vat"]),
                            Total = StringHelper.DaDouble(drLinhas["Discount"]),
                            _Product = new Product
                            {
                                Code = StringHelper.DaString(drLinhas["Product"]),
                                Description = StringHelper.DaString(drLinhas["Product_Desc"]),
                            }
                        };

                        item.Itens.Add(line);
                    }
                    invoices.Add(item);
                }

                return invoices;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public Task<Invoice> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> SaveAsync(Invoice data)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> UpdateAsync(Invoice data)
        {
            throw new NotImplementedException();
        }
    }
}
