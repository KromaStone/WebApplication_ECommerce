using Dapper;
using Data_Library.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ECommerce.Data;

namespace Data_Library.Repository
{
    public class SpCall : ISpCall
    {
        private readonly ApplicationDbContext _Context;
        private static string connectionString = "";
            public SpCall(ApplicationDbContext context)
        {
            _Context = context;
            connectionString = _Context.Database.GetDbConnection().ConnectionString;
        }
        public void excecute(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                sqlcon.Execute(procdureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procdureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                var result = SqlMapper.QueryMultiple(sqlcon, procdureName, param, commandType: CommandType.StoredProcedure);
                var item1 = result.Read<T1>();
                var item2 = result.Read<T2>();
                if (item1 != null && item2 != null)

                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);


                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
            }
        }

        public T oneRecord<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                var value = sqlcon.Query<T>(procdureName, param, commandType: CommandType.StoredProcedure);
                return value.FirstOrDefault();
            }
        }

        public T single<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.ExecuteScalar<T>(procdureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
