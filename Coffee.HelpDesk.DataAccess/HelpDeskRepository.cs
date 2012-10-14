using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Coffee.HelpDesk.Domain;
using Coffee.Shared.Configuration;

namespace Coffee.HelpDesk.DataAccess
{
    public class HelpDeskRepository : IHelpDeskRepository
    {
        private readonly IConfigurationService _configurationService;

        public HelpDeskRepository(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IEnumerable<Department> EnumerateDepartments()
        {
            var result = new List<Department>();
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT DepartmentId, Name FROM Departments";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Department
                                           {
                                               DepartmentId = (Guid)reader["DepartmentId"],
                                               Name = (string)reader["Name"]
                                           });
                        }
                    }
                }
            }

            return result;
        }

        public Guid CreateDepartment(string name)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_CreateDepartment";
                    command.Parameters.AddWithValue("@Name", name).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@DepartmentId", SqlDbType.UniqueIdentifier);
                    returnParameter.Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (returnParameter.Value == DBNull.Value)
                    {
                        throw new InvalidOperationException("Не удалось создать отдел");
                    }

                    return (Guid)returnParameter.Value;
                }
            }
        }

        public bool UpdateDepartment(Guid departmentId, string name)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_UpdateDepartment";
                    command.Parameters.AddWithValue("@DepartmentId", departmentId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Name", name).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public bool DeleteDepartment(Guid departmentId)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_DeleteDepartment";
                    command.Parameters.AddWithValue("@DepartmentId", departmentId).SqlDbType = SqlDbType.UniqueIdentifier;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public Guid? CreateTemplate(Guid departmentId, string template)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_CreateTemplate";
                    command.Parameters.AddWithValue("@DepartmentId", departmentId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Template", template).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@TemplateId", SqlDbType.UniqueIdentifier);
                    returnParameter.Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (returnParameter.Value == DBNull.Value)
                    {
                        return null;
                    }

                    return (Guid)returnParameter.Value;
                }
            }
        }

        public bool UpdateTemplate(Guid templateId, string template)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_UpdateTemplate";
                    command.Parameters.AddWithValue("@TemplateId", templateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    command.Parameters.AddWithValue("@Template", template).SqlDbType = SqlDbType.NVarChar;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public bool DeleteTemplate(Guid templateId)
        {
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_DeleteTemplate";
                    command.Parameters.AddWithValue("@TemplateId", templateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    var returnParameter = command.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value == 1;
                }
            }
        }

        public IEnumerable<HelpDeskTemplate> EnumerateTemplate(Guid departmentId)
        {
            var result = new List<HelpDeskTemplate>();
            using (var connection = new SqlConnection(_configurationService.DatabaseConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT TemplateId, Template FROM DepartmentTemplates WHERE DepartmentId = @DepartmentId";
                    command.Parameters.AddWithValue("@DepartmentId", departmentId).SqlDbType = SqlDbType.UniqueIdentifier;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new HelpDeskTemplate
                            {
                                TemplateId = (Guid)reader["TemplateId"],
                                Template = (string)reader["Template"]
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}