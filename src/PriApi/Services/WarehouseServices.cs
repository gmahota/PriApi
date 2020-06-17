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
    public interface IWarehouseServices
    {
        Task<Warehouse> SaveAsync(Warehouse data);
        Task<Warehouse> UpdateAsync(Warehouse data);
        Task<Warehouse> DeleteAsync(string code);
        Warehouse GetByCodeAsync(string code);

        ICollection<Warehouse> GetAllAsync(WarehouseParams warehouseParams);
    }

    public class WarehouseServices : IWarehouseServices
    {
        public Primavera _Primavera { get; set; }

        public WarehouseServices(IOptions<Primavera> primavera)
        {
            _Primavera = primavera.Value;
        }
        public Task<Warehouse> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }

        public ICollection<Warehouse> GetAllAsync(WarehouseParams warehouseParams)
        {
            try
            {
                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                List<Warehouse> warehouses = new List<Warehouse>();

                DataTable dtWareHouse = db.daListaTabela("Armazens",0, "Armazem as Code, Descricao as Description");

                foreach(DataRow item in dtWareHouse.Rows)
                {
                    var _warehouse = new Warehouse()
                    {
                        Code = item["Code"].ToString(),
                        Description = item["Description"].ToString()
                    };

                    string campos = @"
                         aa.Armazem as Warehouse,aa.StkActual as StkReal ,aa.qtReservada as StkReserved, 
                            aa.StkActual - aa.qtReservada as Stk ,
	                        A.Artigo as Code, A.Descricao as [Description] , 
                                am.PVP1,am.PVP2,am.PVP3,am.PVP4,am.pvp5,am.PVP6
                ";

                    string joins = @"inner join dbo.Artigo A on a.Artigo = aa.Artigo
                                    inner join ArtigoMoeda am on am.Artigo = a.Artigo and am.Moeda like 'M%' ";
                    
                    var filtros = string.Format("aa.Armazem = '{0}'", _warehouse.Code);

                    //if (productParams.Code != null && productParams.Code.Length > 0)
                    //{
                    //    filtros = string.Format(" A.Artigo = '{0}'",
                    //        productParams.Code
                    //    );
                    //}

                    //if (productParams.Description != null && productParams.Description.Length > 0)
                    //{
                    //    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    //    filtros += string.Format("A.Descricao = '{0}' ", productParams.Description);
                    //}

                    //if (productParams.FullSearch != null && productParams.FullSearch.Length > 0)
                    //{
                    //    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    //    filtros += string.Format("A.Artigo like '%{0}%' or A.Descricao like '%{0}%' ", productParams.FullSearch);
                    //}



                    DataTable dt = db.daListaTabela("dbo.ArtigoArmazem aa", 0, campos, filtros, joins, "a.artigo asc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        var product = new ProductModel()
                        {
                            Code = StringHelper.DaString(dr["Code"]),
                            Description = StringHelper.DaString(dr["Description"]),
                            Stk = StringHelper.DaDouble(dr["Stk"]),
                            StkReal = StringHelper.DaDouble(dr["StkReal"]),
                            StkReserved = StringHelper.DaDouble(dr["StkReserved"]),
                            Pvp1 = StringHelper.DaDouble(dr["Pvp1"]),
                            Pvp2 = StringHelper.DaDouble(dr["Pvp2"]),
                            Pvp3 = StringHelper.DaDouble(dr["Pvp3"]),
                            Pvp4 = StringHelper.DaDouble(dr["Pvp4"]),
                            Pvp5 = StringHelper.DaDouble(dr["Pvp5"]),
                            Pvp6 = StringHelper.DaDouble(dr["Pvp6"])
                        };

                        _warehouse.Products.Add(product);
                    }

                    warehouses.Add(_warehouse);
                }                

                return warehouses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Warehouse GetByCodeAsync(string code)
        {
            try
            {
                Warehouse _warehouse = new Warehouse();
                DBPrimavera db = new DBPrimavera(_Primavera.ConnString);

                List<Warehouse> warehouses = new List<Warehouse>();

                DataTable dtWareHouse = db.daListaTabela("Armazens", 0, "Armazem as Code, Descricao as Description",string.Format("Armazem = '{0}'",code));

                foreach (DataRow item in dtWareHouse.Rows)
                {
                    _warehouse = new Warehouse()
                    {
                        Code = item["Code"].ToString(),
                        Description = item["Description"].ToString()
                    };

                    string campos = @"
                         aa.Armazem as Warehouse,aa.StkActual as StkReal ,aa.qtReservada as StkReserved, 
                            aa.StkActual - aa.qtReservada as Stk ,
	                        A.Artigo as Code, A.Descricao as [Description] , 
                                am.PVP1,am.PVP2,am.PVP3,am.PVP4,am.pvp5,am.PVP6
                ";

                    string joins = @"inner join dbo.Artigo A on a.Artigo = aa.Artigo
                                    inner join ArtigoMoeda am on am.Artigo = a.Artigo and am.Moeda like 'M%' ";

                    var filtros = string.Format("aa.Armazem = '{0}'", _warehouse.Code);

                    //if (productParams.Code != null && productParams.Code.Length > 0)
                    //{
                    //    filtros = string.Format(" A.Artigo = '{0}'",
                    //        productParams.Code
                    //    );
                    //}

                    //if (productParams.Description != null && productParams.Description.Length > 0)
                    //{
                    //    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    //    filtros += string.Format("A.Descricao = '{0}' ", productParams.Description);
                    //}

                    //if (productParams.FullSearch != null && productParams.FullSearch.Length > 0)
                    //{
                    //    filtros = filtros.Length == 0 ? "" : filtros + " and ";

                    //    filtros += string.Format("A.Artigo like '%{0}%' or A.Descricao like '%{0}%' ", productParams.FullSearch);
                    //}



                    DataTable dt = db.daListaTabela("dbo.ArtigoArmazem aa", 0, campos, filtros, joins, "a.artigo asc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        var product = new ProductModel()
                        {
                            Code = StringHelper.DaString(dr["Code"]),
                            Description = StringHelper.DaString(dr["Description"]),
                            Stk = StringHelper.DaDouble(dr["Stk"]),
                            StkReal = StringHelper.DaDouble(dr["StkReal"]),
                            StkReserved =  StringHelper.DaDouble(dr["StkReserved"]),
                            Pvp1 = StringHelper.DaDouble(dr["Pvp1"]),
                            Pvp2 = StringHelper.DaDouble(dr["Pvp2"]),
                            Pvp3 = StringHelper.DaDouble(dr["Pvp3"]),
                            Pvp4 = StringHelper.DaDouble(dr["Pvp4"]),
                            Pvp5 = StringHelper.DaDouble(dr["Pvp5"]),
                            Pvp6 = StringHelper.DaDouble(dr["Pvp6"])
                        };

                        _warehouse.Products.Add(product);
                    }
                    break;
                    
                }

                return _warehouse;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Warehouse> SaveAsync(Warehouse data)
        {
            throw new NotImplementedException();
        }

        public Task<Warehouse> UpdateAsync(Warehouse data)
        {
            throw new NotImplementedException();
        }
    }
}
