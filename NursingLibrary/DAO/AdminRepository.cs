using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.DAO
{
    public class AdminRepository : IAdminRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;

        #endregion Fields

        #region Constructors

        public AdminRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #endregion

        #region IAdminRepository Members

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <returns>IEnumerable<SimpleProduct></returns>
        public IEnumerable<Product> GetProducts(int productId)
        {
            var products = new List<Product>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetAllProducts", sqlParameters))
            {
                while (reader.Read())
                {
                    var content = new Product
                    {
                        ProductId = (reader["ProductID"] as int?).GetValueOrDefault(0),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        ProductType = Convert.ToString(reader["ProductType"]),
                        Bundle = (reader["Bundle"] as int?) ?? 0,
                        MultiUseTest = (reader["MultiUseTest"] as int?) ?? 0,
                        Remediation = (reader["Remediation"] as int?) ?? 0,
                        Scramble = (reader["Scramble"] as int?) ?? 0,
                        TestType = (reader["TestType"] as string) ?? string.Empty
                    };
                    products.Add(content);
                }
            }

            return products;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>();
            using (IDataReader reader = _dataContext.GetDataReader("UspGetAllCategories"))
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (reader["CategoryID"] as int?) ?? 0,
                        TableName = (reader["TableName"] as string) ?? string.Empty,
                        OrderNumber = (reader["OrderNumber"] as int?) ?? 0,
                        ProgramofStudyId = (reader["programofStudyId"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty
                    });
                }
            }

            return categories;
        }

        public IEnumerable<CategoryDetail> GetCategoryDetails(int categoryId, int programOfStudyIdForCategory)
        {
            var categoryDetails = new List<CategoryDetail>();

            string spName = string.Empty;
            bool hasParentId = false;

            // Has to live with this switch case. This cannot be avoided since data is stored in different tables.
            // Extracting to independent methods may help only from code refactoring point of view.
            switch (categoryId)
            {
                case 1:
                case 13:
                    {
                        spName = "UspGetClientNeedsCategory";
                        break;
                    }

                case 2:
                case 14:
                    {
                        spName = "UspGetNursingProcessCategory";
                        break;
                    }

                case 3:
                case 15:
                    {
                        spName = "UspGetCriticalThinkingCategory";
                        break;
                    }

                case 4:
                case 16:
                    {
                        spName = "UspGetClinicalConceptCategory";
                        break;
                    }

                case 5:
                case 17:
                    {
                        spName = "UspGetDemographicCategory";
                        break;
                    }

                case 6:
                case 18:
                    {
                        spName = "UspGetCognitiveLevelCategory";
                        break;
                    }

                case 7:
                case 19:
                    {
                        spName = "UspGetSpecialtyAreaCategory";
                        break;
                    }

                case 8:
                case 20:
                    {
                        spName = "UspGetSystemsCategory";
                        break;
                    }

                case 9:
                case 21:
                    {
                        spName = "UspGetLevelOfDifficultyCategory";
                        break;
                    }

                case 10:
                case 22:
                    {
                        spName = "UspGetClientNeedCategoryCategory";
                        hasParentId = true;
                        break;
                    }

                case 11:
                    {
                        spName = "UspGetAccreditationCategoriesCategory";
                        break;
                    }

                case 12:
                    {
                        spName = "UspGetQSENKSACompetenciesCategory";
                        break;
                    }

                case 23:
                case 24:
                    {
                        spName = "UspGetConceptsCategory";
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Category ID - {0} is not within the accepted range. Please check the argument passed in.", categoryId));
                    }
            }

            using (IDataReader reader = _dataContext.GetDataReader(spName))
            {
                while (reader.Read())
                {
                    var programOfStudyId = (reader["ProgramofStudyId"] as int?) ?? 0;
                    if (programOfStudyId.Equals(programOfStudyIdForCategory))
                    {
                        categoryDetails.Add(new CategoryDetail
                        {
                            Id = (reader["Id"] as int?) ?? 0,
                            Description = (reader["Description"] as string) ?? string.Empty,
                            ParentId = hasParentId ? reader["ParentId"] as int? ?? 0 : 0,
                            ProgramofStudy = programOfStudyId
                        });
                    }
                }
            }

            return categoryDetails;
        }

        public IDictionary<AppSettings, string> GetAppSettings()
        {
            var settings = new Dictionary<AppSettings, string>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAppSettings"))
            {
                while (reader.Read())
                {
                    settings.Add((AppSettings)Enum.Parse(typeof(AppSettings), reader["SettingsId"].ToString()),
                        reader["Value"].ToString());
                }
            }

            return settings;
        }

        public void SaveAppSetting(AppSettings setting, string value)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@settingsId", SqlDbType.SmallInt) { Value = (int)setting };
            sqlParameters[1] = new SqlParameter("@value", SqlDbType.VarChar, 200) { Value = value };

            _dataContext.ExecuteStoredProcedure("uspSaveAppSettings", sqlParameters);
        }

        public List<Institution> GetInstitutions(int userId, string institutionIds)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = 0 };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    institutions.Add(new Institution
                    {
                        ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty,
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        InstitutionName = (reader["InstitutionName"] as String) ?? string.Empty,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        Description = (reader["Description"] as String) ?? string.Empty,
                        ContactName = (reader["ContactName"] as String) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as String) ?? string.Empty,
                        CenterId = (reader["CenterID"] as String) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        IP = (reader["IP"] as String) ?? string.Empty,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        InstitutionAddress = new Address { AddressId = (reader["AddressID"] as int?) ?? 0, },
                        Annotation = (reader["Annotation"] as String) ?? string.Empty,
                        ContractualStartDate = (reader["ContractualStartDate"] as DateTime?).ToString() ?? string.Empty,
                        Email = (reader["Email"] as String) ?? string.Empty,
                        PayLinkEnabled = (bool)(reader["PayLinkEnabled"] is DBNull ? false : reader["PayLinkEnabled"])
                    });
                }
            }

            return institutions.ToList();
        }

        public List<Institution> GetAllInstitution()
        {
            var institutions = new List<Institution>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAllInstitutions"))
            {
                while (reader.Read())
                {
                    institutions.Add(new Institution
                      {
                          InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                          InstitutionName = (reader["InstitutionName"] as String) ?? string.Empty,
                          ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty

                      });
                }
            }

            return institutions.ToList();
        }



        public List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = 0 };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            sqlParameters[3] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofStudyId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);
                    institutions.Add(new Institution
                    {
                        ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty,
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        InstitutionName = (reader["InstitutionName"] as String) ?? string.Empty,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        Description = (reader["Description"] as String) ?? string.Empty,
                        ContactName = (reader["ContactName"] as String) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as String) ?? string.Empty,
                        CenterId = (reader["CenterID"] as String) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        IP = (reader["IP"] as String) ?? string.Empty,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        InstitutionAddress = new Address { AddressId = (reader["AddressID"] as int?) ?? 0, },
                        Annotation = (reader["Annotation"] as String) ?? string.Empty,
                        ContractualStartDate = (reader["ContractualStartDate"] as DateTime?).ToString() ?? string.Empty,
                        Email = (reader["Email"] as String) ?? string.Empty,
                        PayLinkEnabled = (bool)(reader["PayLinkEnabled"] is DBNull ? false : reader["PayLinkEnabled"]),
                        ProctorTrackSecurityEnabled = (reader["ProctorTrackEnabled"] as int?) ?? 0
                    });
                }
            }

            return institutions.ToList();
        }

        public IEnumerable<Program> SearchPrograms(int programOfStudyId, string searchText)
        {
            var programs = new List<Program>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProgramofStudyId", SqlDbType.SmallInt, 6) { Value = programOfStudyId };
            sqlParameters[1] = new SqlParameter("@SearchText", SqlDbType.VarChar, 200) { Value = searchText };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchPrograms", sqlParameters))
            {
                while (reader.Read())
                {
                    programs.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        ProgramName = (reader["ProgramName"] as string) ?? string.Empty,
                        Description = (reader["Description"] as string) ?? string.Empty
                    });
                }
            }

            return programs.ToArray();
        }

        public void SaveProgram(Program program, int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = program.ProgramId, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@ProgramName", SqlDbType.VarChar, 200) { Value = program.ProgramName };
            sqlParameters[2] = new SqlParameter("@Description", SqlDbType.VarChar, 200) { Value = program.Description };
            sqlParameters[3] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[4] = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int, 6) { Value = program.ProgramOfStudyId };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveProgram", sqlParameters);
            program.ProgramId = (int)sqlParameters[0].Value;
        }

        public void DeleteProgram(int programId, int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPDeleteProgram", sqlParameters);
        }

        public void DeleteProductFromProgram(int programId, int productId, int type, int assetGroupId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@ProductId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[2] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type };
            sqlParameters[3] = new SqlParameter("@AssetGroupId", SqlDbType.Int, 4) { Value = assetGroupId };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPDeleteProgramFromProduct", sqlParameters);
        }

        public Program GetProgram(int programId)
        {
            var program = new Program();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetProgram", sqlParameters))
            {
                if (reader.Read())
                {
                    program.ProgramId = (reader["ProgramID"] as int?) ?? 0;
                    program.ProgramName = (reader["ProgramName"] as string) ?? string.Empty;
                    program.Description = (reader["Description"] as string) ?? string.Empty;
                    program.ProgramOfStudyId = (reader["ProgramOfStudyId"] as int?) ?? 0;
                    program.ProgramOfStudyName = (reader["ProgramOfStudyName"] as string) ?? string.Empty;
                }
            }

            return program;
        }

        public IEnumerable<TimeZones> GetTimeZones()
        {
            var timeZones = new List<TimeZones>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[0];
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTimeZones", sqlParameters))
            {
                while (reader.Read())
                {
                    timeZones.Add(new TimeZones
                    {
                        TimeZoneId = (reader["TimeZoneID"] as int?) ?? 0,
                        TimeZoneName = (reader["TimeZoneName"] as String) ?? string.Empty,
                        Description = (reader["Description"] as String) ?? string.Empty,
                    });
                }
            }

            return timeZones.ToArray();
        }

        public IEnumerable<Program> GetPrograms()
        {
            var nurPrograms = new List<Program>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[0];
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetPrograms", sqlParameters))
            {
                while (reader.Read())
                {
                    nurPrograms.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        ProgramName = (reader["ProgramName"] as String) ?? string.Empty,
                        Description = (reader["Description"] as String) ?? string.Empty,
                    });
                }
            }

            return nurPrograms.ToArray();
        }

        public void SaveInstitution(Institution institution)
        {
            int userId = 0;
            #region SqlParameters
            var sqlParameters = new SqlParameter[20];

            var programOfStudyIdID = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int, 6) { Value = institution.ProgramOfStudyId };
            sqlParameters[0] = programOfStudyIdID;

            var parameterInstitutionID = new SqlParameter("@InstitutionID", SqlDbType.Int, 6) { Value = institution.InstitutionId, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = parameterInstitutionID;

            var parameterInstitutionName = new SqlParameter("@InstitutionName", SqlDbType.NVarChar, 80) { Value = institution.InstitutionName };
            sqlParameters[2] = parameterInstitutionName;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 80) { Value = institution.Description };
            sqlParameters[3] = parameterDescription;

            var parameterContactName = new SqlParameter("@ContactName", SqlDbType.NVarChar, 50) { Value = institution.ContactName };
            sqlParameters[4] = parameterContactName;

            var parameterContactPhone = new SqlParameter("@ContactPhone", SqlDbType.VarChar, 50) { Value = institution.ContactPhone };
            sqlParameters[5] = parameterContactPhone;

            var parameterTimeZone = new SqlParameter("@TimeZone", SqlDbType.Int, 6) { Value = institution.TimeZone };
            sqlParameters[6] = parameterTimeZone;

            var parameterIP = new SqlParameter("@IP", SqlDbType.VarChar, 250) { Value = institution.IP };
            sqlParameters[7] = parameterIP;

            var parameterFacilityID = new SqlParameter("@FacilityID", SqlDbType.Int, 6) { Value = institution.FacilityID };
            sqlParameters[8] = parameterFacilityID;

            var parameterCenterID = new SqlParameter("@CenterID", SqlDbType.NVarChar, 50) { Value = institution.CenterId };
            sqlParameters[9] = parameterCenterID;

            var parameterProgramID = new SqlParameter("@ProgramID", SqlDbType.Int, 6) { Value = institution.ProgramID };
            sqlParameters[10] = parameterProgramID;

            if (institution.InstitutionId == 0)
            {
                userId = institution.CreateUser;
            }
            else
            {
                userId = institution.UpdateUser;
            }

            var parameterUser = new SqlParameter("@CreateOrUpdatedUser", SqlDbType.Int, 6) { Value = userId };
            sqlParameters[11] = parameterUser;

            var parameterDeleteUser = new SqlParameter("@DeleteUser", SqlDbType.Int, 6) { Value = institution.DeleteUser };
            sqlParameters[12] = parameterDeleteUser;

            var parameterAddressID = new SqlParameter("@AddressID", SqlDbType.Int, 6) { Value = institution.AddressId };
            sqlParameters[13] = parameterAddressID;

            var parameterStatus = new SqlParameter("@Status", SqlDbType.Int, 6) { Value = institution.Status };
            sqlParameters[14] = parameterStatus;

            var parameterAnnotation = new SqlParameter("@Annotation", SqlDbType.VarChar, 1000) { Value = institution.AnnotationSave };
            sqlParameters[15] = parameterAnnotation;

            var parameterContractualStartDate = new SqlParameter("@ContractualStartDate", SqlDbType.SmallDateTime) { Value = String.IsNullOrEmpty(institution.ContractualStartDate) ? Convert.DBNull : Convert.ToDateTime(institution.ContractualStartDate) };
            sqlParameters[16] = parameterContractualStartDate;

            sqlParameters[17] = new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = institution.Email };

            sqlParameters[18] = new SqlParameter("@PayLinkEnabled", SqlDbType.Bit, 100) { Value = institution.PayLinkEnabled };
            sqlParameters[19] = new SqlParameter("@ProctorTrackSecurityEnabled", SqlDbType.Int, 2) { Value = institution.ProctorTrackSecurityEnabled };
            #endregion
            _dataContext.ExecuteStoredProcedure("USPSaveInstitution", sqlParameters);
            institution.InstitutionId = (int)sqlParameters[1].Value;
        }

        public IEnumerable<Group> SearchGroups(string searchText, string institutionIds, string cohortIds)
        {
            var groups = new List<Group>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@SearchString", SqlDbType.VarChar) { Value = searchText };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[2] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchGroups", sqlParameters))
            {
                while (reader.Read())
                {
                    var instititutionNameWithProgramOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    groups.Add(new Group
                    {
                        GroupId = (reader["GroupID"] as int?) ?? 0,
                        GroupName = (reader["GroupName"] as string) ?? string.Empty,
                        Cohort = new Cohort() { CohortName = (reader["CohortName"] as string) ?? string.Empty, CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution() { InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty, InstitutionNameWithProgOfStudy = instititutionNameWithProgramOfStudy },
                    });
                }
            }

            return groups.ToArray();
        }

        public IEnumerable<Group> GetGroups(int groupId, string cohortIds)
        {
            var groups = new List<Group>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar, 400) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetGroup", sqlParameters))
            {
                while (reader.Read())
                {
                    groups.Add(new Group
                    {
                        GroupId = (reader["GroupID"] as int?) ?? 0,
                        GroupName = (reader["GroupName"] as string) ?? string.Empty,
                        Cohort = new Cohort() { CohortId = (reader["CohortId"] as int?) ?? 0 },
                    });
                }
            }

            return groups.ToArray();
        }

        public int SaveGroup(Group group)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 100) { Value = group.GroupName };
            sqlParameters[1] = new SqlParameter("@CohortID", SqlDbType.Int, 4) { Value = group.Cohort.CohortId };
            sqlParameters[2] = new SqlParameter("@GroupUserID", SqlDbType.Int, 4) { Value = group.GroupUserId };
            sqlParameters[3] = new SqlParameter("@GroupID", SqlDbType.Int, 4) { Value = group.GroupId };
            sqlParameters[4] = new SqlParameter("@newGroupID", SqlDbType.Int, 4);
            sqlParameters[4].Direction = ParameterDirection.Output;
            #endregion
            _dataContext.ExecuteNonQuery("USPSaveGroup", sqlParameters);
            int newGroupId = (int)sqlParameters[4].Value;
            return newGroupId;
        }

        public int GetInstitutionIdForCohort(int cohortId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            int institutionId = 0;
            sqlParameters[0] = new SqlParameter("@CohortID", SqlDbType.Int, 4) { Value = cohortId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("dbo.USPGetInstitutionIdForCohort", sqlParameters))
            {
                if (reader.Read())
                {
                    institutionId = (reader["InstitutionID"] as int?) ?? 0;
                }
            }

            return institutionId;
        }

        public int DeleteGroup(int groupId, int groupDeleteUser)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[1] = new SqlParameter("@GroupDeleteUser", SqlDbType.Int, 4) { Value = groupDeleteUser };
            #endregion
            return _dataContext.ExecuteNonQuery("USPDeleteGroup", sqlParameters);
        }

        public IEnumerable<Institution> SearchInstitutes(string InstitutionIds)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@institutionIds", SqlDbType.VarChar, 400) { Value = InstitutionIds };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetListOfInstitution", sqlParameters))
            {
                if (!reader.Read())
                {
                    reader.NextResult();
                }

                while (reader.Read())
                {
                    institutions.Add(new Institution
                    {
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        Description = (reader["Description"] as string) ?? string.Empty,
                        Status = (reader["Status"] as string) ?? string.Empty,
                        ContactName = (reader["ContactName"] as string) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as string) ?? string.Empty,
                        IP = (reader["IP"] as string) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        UpdateUser = (reader["UpdateUser"] as int?) ?? 0,
                        CreateUser = (reader["CreateUser"] as int?) ?? 0,
                        DeleteUser = (reader["DeleteUser"] as int?) ?? 0,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                    });
                }
            }

            return institutions.ToArray();
        }

        public IEnumerable<Institution> GetInstitutes(int instituteId, string instituteIds, int userId)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = instituteId };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 400) { Value = instituteIds };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    institutions.Add(new Institution
                    {
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        Description = (reader["Description"] as string) ?? string.Empty,
                        ContactName = (reader["ContactName"] as string) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as string) ?? string.Empty,
                        IP = (reader["IP"] as string) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        CenterId = (reader["CenterID"] as string) ?? string.Empty,
                        Active = (reader["ProgramID"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        InstitutionAddress = new Address { AddressId = (reader["AddressID"] as int?) ?? 0, },
                        Annotation = (reader["Annotation"] as String) ?? string.Empty,
                        ContractualStartDate = (reader["ContractualStartDate"] as DateTime?).ToString() ?? string.Empty,
                        PayLinkEnabled = (bool)(reader["PayLinkEnabled"] is DBNull ? false : reader["PayLinkEnabled"]),
                    });
                }
            }

            return institutions.ToArray();
        }

        public IEnumerable<Institution> SearchInstitutes(string searchText, int userId)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@SearchText", SqlDbType.VarChar, 200) { Value = searchText };
            sqlParameters[1] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    institutions.Add(new Institution
                    {
                        ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty,
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        Description = (reader["Description"] as String) ?? string.Empty,
                        ContactName = (reader["ContactName"] as String) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as String) ?? string.Empty,
                        CenterId = (reader["CenterID"] as String) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        IP = (reader["IP"] as String) ?? string.Empty,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        TimeZones = new TimeZones { Description = (reader["TZD"] as String) ?? string.Empty, TimeZoneId = (reader["TimeZone"] as int?) ?? 0 }
                    });
                }
            }

            return institutions.ToArray();
        }

        public IEnumerable<UserDetails> SearchUserDetails(string searchText, string status, int ProgramofStudy)
        {
            var UserDetails = new List<UserDetails>();
            var sqlParameters = new SqlParameter[3];
            #region SqlParameters
            sqlParameters[0] = new SqlParameter("@SearchText", SqlDbType.VarChar, 200) { Value = searchText };
            sqlParameters[1] = new SqlParameter("@Usertype", SqlDbType.VarChar, 4) { Value = status };
            sqlParameters[2] = new SqlParameter("@ProgramOfStudy", SqlDbType.Int) { Value = ProgramofStudy };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspSearchUserDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);
                    UserDetails.Add(new UserDetails
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                        UserPass = (reader["UserPass"] as String) ?? string.Empty,
                        FirstName = (reader["FirstName"] as String) ?? string.Empty,
                        LastName = (reader["LastName"] as String) ?? string.Empty,
                        InstitutionName = institutionNameWithProgOfStudy,
                    });
                }
            }
            return UserDetails.ToArray();
        }

        public IEnumerable<UserDetails> SearchUnassignedUserDetails(string searchText, string status, int ProgramofStudy)
        {
            var UserDetails = new List<UserDetails>();
            var sqlParameters = new SqlParameter[2];

            #region SqlParameters
            sqlParameters[0] = new SqlParameter("@SearchText", SqlDbType.VarChar, 400) { Value = searchText };
            sqlParameters[1] = new SqlParameter("@Usertype", SqlDbType.VarChar, 4) { Value = status };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPSearchUnAssignedUserDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    UserDetails.Add(new UserDetails
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                        UserPass = (reader["UserPass"] as String) ?? string.Empty,
                        FirstName = (reader["FirstName"] as String) ?? string.Empty,
                        LastName = (reader["LastName"] as String) ?? string.Empty,
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                    });
                }
            }
            return UserDetails.ToArray();
        }

        public IEnumerable<Cohort> SearchCohorts(string institutionIds, string searchString, int TestId, string dateFrom, string dateTo, int cohortStatus, int programofStudyId)
        {
            var cohorts = new List<Cohort>();

            #region SqlParameters
            var sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 8000) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchString };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int) { Value = TestId };
            sqlParameters[3] = new SqlParameter("@DateFrom", SqlDbType.VarChar, 400) { Value = dateFrom };
            sqlParameters[4] = new SqlParameter("@DateTo", SqlDbType.VarChar, 400) { Value = dateTo };
            sqlParameters[5] = new SqlParameter("@CohortStatus", SqlDbType.Int, 400) { Value = cohortStatus };
            sqlParameters[6] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofStudyId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchCohorts", sqlParameters))
            {
                while (reader.Read())
                {
                    var instititutionNameWithProgramOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    cohorts.Add(new Cohort
                    {
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                        CohortDescription = (reader["CohortDescription"] as string) ?? string.Empty,
                        CohortStartDate = (reader["CohortStartDate"] as DateTime?) ?? null,
                        CohortEndDate = (reader["CohortEndDate"] as DateTime?) ?? null,
                        Institution = new Institution()
                        {
                            InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                            Annotation = (reader["Annotation"] as string) ?? string.Empty,
                            InstitutionNameWithProgOfStudy = instititutionNameWithProgramOfStudy
                        },
                        StudentCount = (reader["Students"] as int?) ?? 0,
                        RepeatingStudentCount = (reader["RepeatingStudentCount"] as int?) ?? 0
                    });
                }
            }

            return cohorts.ToArray();
        }

        public IEnumerable<Cohort> GetCohorts(int cohortId, string institutionIds)
        {
            var cohorts = new List<Cohort>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCohorts", sqlParameters))
            {
                while (reader.Read())
                {
                    cohorts.Add(new Cohort
                    {
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        CohortStartDate = (reader["CohortStartDate"] as DateTime?) ?? null,
                        CohortEndDate = (reader["CohortEndDate"] as DateTime?) ?? null,
                        CohortStatus = (reader["CohortStatus"] as int?) ?? 0,
                        CohortDescription = (reader["CohortDescription"] as string) ?? string.Empty,
                        ProgramofStudyId = (reader["ProgramOfStudyId"] as int?) ?? 0,
                    });
                }
            }

            return cohorts.ToArray();
        }

        public int SaveCohort(Cohort cohort)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[9];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohort.CohortId };
            sqlParameters[1] = new SqlParameter("@CohortName", SqlDbType.VarChar, 160) { Value = cohort.CohortName };
            sqlParameters[2] = new SqlParameter("@CohortDescription", SqlDbType.VarChar, 1000) { Value = cohort.CohortDescription };
            sqlParameters[3] = new SqlParameter("@CohortStatus", SqlDbType.Int, 4) { Value = cohort.CohortStatus };
            sqlParameters[4] = new SqlParameter("@CohortStartDate", SqlDbType.DateTime, 0) { Value = cohort.CohortStartDate ?? Convert.DBNull };
            sqlParameters[5] = new SqlParameter("@CohortEndDate", SqlDbType.DateTime, 0) { Value = cohort.CohortEndDate ?? Convert.DBNull };
            sqlParameters[6] = new SqlParameter("@CohortCreateUser", SqlDbType.Int, 4) { Value = cohort.CohortCreateUser };
            sqlParameters[7] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = cohort.InstitutionId };
            sqlParameters[8] = new SqlParameter("NewCohortId", SqlDbType.Int, 4);
            sqlParameters[8].Direction = ParameterDirection.Output;

            _dataContext.ExecuteNonQuery("USPSaveCohort", sqlParameters);
            int newGroupId = (int)sqlParameters[8].Value;
            return newGroupId;
            #endregion
        }

        public void DeleteCohort(int cohortId, int cohortStatus, int cohortDeleteUser)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[1] = new SqlParameter("@CohortStatus", SqlDbType.Int, 4) { Value = cohortStatus };
            sqlParameters[2] = new SqlParameter("@CohortDeleteUser", SqlDbType.Int, 4) { Value = cohortDeleteUser };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPDeleteCohort", sqlParameters);
        }

        public IEnumerable<Student> SearchStudent(int programOfStudyId, int institutionId, int cohortId, int groupId, string searchString, bool assignStudent)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@programOfStudyId", SqlDbType.Int, 4) { Value = programOfStudyId };
            sqlParameters[1] = new SqlParameter("@institutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[2] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[3] = new SqlParameter("@groupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[4] = new SqlParameter("@searchString", SqlDbType.VarChar, 100) { Value = searchString };
            sqlParameters[5] = new SqlParameter("@AssignStudent", SqlDbType.Bit) { Value = assignStudent };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPSearchStudent", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        KaplanUserId = reader["KaplanUserID"] as string ?? string.Empty,
                        Ada = (bool)(reader["Ada"] is DBNull ? false : reader["Ada"]),
                        UserPass = reader["UserPass"] as string ?? string.Empty,
                        UserCreateDate = Convert.ToDateTime(reader["UserCreateDate"]),
                        EnrollmentId = reader["EnrollmentID"] as string ?? string.Empty,
                        CountryCode = reader["CountryCode"] as string ?? string.Empty,
                        Cohort = new Cohort() { CohortName = (reader["CohortName"] as string) ?? string.Empty, CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution()
                                        {
                                            InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                                            InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                                            InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy
                                        },
                        Group = new Group() { GroupName = (reader["GroupName"] as string) ?? string.Empty, GroupId = (reader["GroupId"] as int?) ?? 0 },
                        Email = reader["Email"] as string ?? string.Empty,
                        IsProctorTrackEnabled = (reader["ProctorTrackEnabled"] as int?) ?? 0
                    });
                }
            }

            return students;
        }

        public IEnumerable<Student> GetStudentsInCohortAndGroups(int institutionId, int cohortId, int groupId)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudentsForCohort", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        Program = new Program() { ProgramName = (reader["ProgramName"] as string) ?? string.Empty, DeletedDate = (reader["P_DeletedDate"] as DateTime?) ?? null },
                        Cohort = new Cohort() { CohortName = (reader["CohortName"] as string) ?? string.Empty, CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution()
                            {
                                InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                                InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                                Active = (reader["IsAssigned"] as int?) ?? 0,
                                InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                            },
                        Group = new Group() { GroupName = (reader["GroupName"] as string) ?? string.Empty, GroupId = (reader["GroupId"] as int?) ?? 0 },
                    });
                }
            }

            return students;
        }

        public void AssignStudentsToGroup(int groupId, string assignStudentList, string unassignedStudentList)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[1] = new SqlParameter("@AssignStudentList", SqlDbType.VarChar, 4000) { Value = assignStudentList };
            sqlParameters[2] = new SqlParameter("@UnassignedStudentList", SqlDbType.VarChar, 4000) { Value = unassignedStudentList };
            #endregion
            _dataContext.ExecuteNonQuery("USPAssignStudentToGroup", sqlParameters);
        }

        public IEnumerable<Group> GetGroupsList(int institutionId, int cohortId)
        {
            var groups = new List<Group>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetGroupsList", sqlParameters))
            {
                while (reader.Read())
                {
                    groups.Add(new Group
                    {
                        GroupId = (reader["GroupID"] as int?) ?? 0,
                        GroupName = (reader["GroupName"] as string) ?? string.Empty,
                        Cohort = new Cohort() { CohortName = (reader["CohortName"] as string) ?? string.Empty, CohortStatus = (reader["CohortStatus"] as int?) ?? 0 },
                        Institution = new Institution() { InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty, InstitutionId = (reader["InstitutionID"] as int?) ?? 0 },
                        Program = new Program() { ProgramName = (reader["ProgramName"] as string) ?? string.Empty, ProgramId = (reader["ProgramID"] as int?) ?? 0 },
                    });
                }
            }

            return groups.ToArray();
        }

        public IEnumerable<Student> GetStudents(int studentId, string searchText)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentId };

            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudents", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        KaplanUserId = reader["KaplanUserID"] as string ?? string.Empty,
                        EnrollmentId = reader["EnrollmentID"] as string ?? string.Empty,
                        Ada = (bool)(reader["Ada"] is DBNull ? false : reader["Ada"]),
                        UserPass = reader["UserPass"] as string ?? string.Empty,
                        Telephone = reader["Telephone"] as string ?? string.Empty,
                        Email = reader["Email"] as string ?? string.Empty,
                        ContactPerson = reader["ContactPerson"] as string ?? string.Empty,
                        EmergencyPhone = reader["EmergencyPhone"] as string ?? string.Empty,
                        UserType = reader["UserType"] as string ?? string.Empty,
                        UserCreateDate = Convert.ToDateTime(reader["UserCreateDate"]),
                        UserExpireDate = (DateTime?)(reader["UserExpireDate"] is DBNull ? null : reader["UserExpireDate"]),
                        UserStartDate = (DateTime?)(reader["UserStartDate"] is DBNull ? null : reader["UserStartDate"]),
                        UserUpdateDate = (DateTime?)(reader["UserUpdateDate"] is DBNull ? null : reader["UserUpdateDate"]),
                        Cohort = new Cohort() { CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution()
                                          {
                                              InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                                              ProgramOfStudyId = (reader["ProgramOfStudyId"] as int?) ?? 0,
                                              ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty
                                          },
                        Group = new Group() { GroupId = (reader["GroupId"] as int?) ?? 0 },
                        StudentAddress = new Address() { AddressId = (reader["AddressID"] as int?) ?? 0 },
                        RepeatExpiryDate = (reader["RepeatExpiryDate"] is DBNull ? null : reader["RepeatExpiryDate"].ToString()),
                    });
                }
            }

            return students.ToArray();
        }

        public StudentEntity GetDatesByCohortId(int cohortId)
        {
            var student = new StudentEntity();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = cohortId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetDatesForCohort", sqlParameters))
            {
                if (reader.Read())
                {
                    student.StartDate = Convert.ToString(reader["CohortStartDate"]);
                    student.EndDate = Convert.ToString(reader["CohortEndDate"]);
                }
            }

            return student;
        }

        public int SaveUser(Student student, int AdminUserId, string AdminUserName)
        {
            var sqlParameters = new SqlParameter[23];
            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.Int, 200) { Value = student.UserId };
            sqlParameters[1] = new SqlParameter("@username", SqlDbType.VarChar, 255) { Value = student.UserName };
            sqlParameters[2] = new SqlParameter("@userpass", SqlDbType.VarChar, 255) { Value = student.UserPass };
            sqlParameters[3] = new SqlParameter("@email", SqlDbType.VarChar, 200) { Value = student.Email };
            sqlParameters[4] = new SqlParameter("@institutionId", SqlDbType.Int, 4) { Value = student.Institution.InstitutionId };
            sqlParameters[5] = new SqlParameter("@integrated", SqlDbType.VarChar, 255) { Value = student.Integreted };
            sqlParameters[6] = new SqlParameter("@kaplanUserId", SqlDbType.VarChar, 200) { Value = student.KaplanUserId };
            sqlParameters[7] = new SqlParameter("@enrollmentId", SqlDbType.VarChar, 255) { Value = student.EnrollmentId };
            sqlParameters[8] = new SqlParameter("@expireDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(student.ExpireDate) ? Convert.DBNull : Convert.ToDateTime(student.ExpireDate) };
            sqlParameters[9] = new SqlParameter("@startDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(student.StartDate) ? Convert.DBNull : Convert.ToDateTime(student.StartDate) };
            sqlParameters[10] = new SqlParameter("@firstname", SqlDbType.VarChar, 200) { Value = student.FirstName };
            sqlParameters[11] = new SqlParameter("@lastname", SqlDbType.VarChar, 200) { Value = student.LastName };
            sqlParameters[12] = new SqlParameter("@ada", SqlDbType.Bit) { Value = student.Ada };
            sqlParameters[13] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = student.Cohort.CohortId };
            sqlParameters[14] = new SqlParameter("@groupId", SqlDbType.Int, 4) { Value = student.Group.GroupId };
            sqlParameters[15] = new SqlParameter("@addressID", SqlDbType.Int, 4) { Value = student.AddressId };
            sqlParameters[16] = new SqlParameter("@contactPerson", SqlDbType.VarChar, 255) { Value = student.ContactPerson ?? string.Empty };
            sqlParameters[17] = new SqlParameter("@emergencyPhone", SqlDbType.VarChar, 255) { Value = student.EmergencyPhone ?? string.Empty };
            sqlParameters[18] = new SqlParameter("@telephone", SqlDbType.VarChar, 255) { Value = student.Telephone ?? string.Empty };
            sqlParameters[19] = new SqlParameter("@NewUserId", SqlDbType.Int, 4);
            sqlParameters[19].Direction = ParameterDirection.Output;
            sqlParameters[20] = new SqlParameter("@adminUserId", SqlDbType.Int, 100) { Value = AdminUserId };
            sqlParameters[21] = new SqlParameter("@adminUserName", SqlDbType.VarChar, 255) { Value = AdminUserName };
            sqlParameters[22] = new SqlParameter("@repeatExpiryDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(student.RepeatExpiryDate) ? Convert.DBNull : Convert.ToDateTime(student.RepeatExpiryDate) };
            _dataContext.ExecuteNonQuery("USPSaveUser", sqlParameters);
            int userId = (int)sqlParameters[19].Value;
            return userId;
        }

        public void DeleteUser(int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };

            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteUser", sqlParameters);
        }

        public IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 1000) { Value = institutionIds };
            sqlParameters[3] = new SqlParameter("@ForCMS", SqlDbType.Bit, 1000) { Value = forCMS };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["Name"] as string) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        Product = new Product() { ProductName = (reader["ProductName"] as string) ?? string.Empty }
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 1000) { Value = institutionIds };
            sqlParameters[3] = new SqlParameter("@ForCMS", SqlDbType.Bit, 1000) { Value = forCMS };
            sqlParameters[4] = new SqlParameter("@ProgramofStudy", SqlDbType.Int, 4) { Value = programofStudy };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["Name"] as string) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        Product = new Product() { ProductName = (reader["ProductName"] as string) ?? string.Empty }
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<Program> GetCohortProgram(int CohortProgramId, int ProgramId, int CohortId)
        {
            var cohortPrograms = new List<Program>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CohortProgramId", SqlDbType.Int, 4) { Value = CohortProgramId };
            sqlParameters[1] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = ProgramId };
            sqlParameters[2] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = CohortId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetCohortProgram", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortPrograms.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        Cohort = new Cohort() { CohortId = (reader["CohortID"] as int?) ?? 0 },
                    });
                }
            }

            return cohortPrograms.ToArray();
        }

        public IEnumerable<Program> SearchCohortPrograms(int CohortId, string SearchText)
        {
            var cohortPrograms = new List<Program>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = CohortId };
            sqlParameters[1] = new SqlParameter("@SearchText", SqlDbType.VarChar, 200) { Value = SearchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPSearchCohortProgram", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortPrograms.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        ProgramName = (reader["ProgramName"] as string) ?? string.Empty,
                        Description = (reader["Description"] as string) ?? string.Empty,
                        Cohort = new Cohort() { CohortId = (reader["CohortID"] as int?) ?? 0 },
                    });
                }
            }

            return cohortPrograms.ToArray();
        }

        public void SaveCohortProgram(int CohortId, int ProgramId, int Active)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = CohortId };
            sqlParameters[1] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = ProgramId };
            sqlParameters[2] = new SqlParameter("@Active", SqlDbType.Int, 4) { Value = Active };
            _dataContext.ExecuteStoredProcedure("USPAssignCohortProgram", sqlParameters);
        }

        public void AssignTestDateToCohort(CohortTestDates testDate)
        {
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = testDate.Cohort.CohortId };
            sqlParameters[1] = new SqlParameter("@ProductId", SqlDbType.Int, 4) { Value = testDate.Product.ProductId };
            sqlParameters[2] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = testDate.Type };
            sqlParameters[3] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestStartDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestStartDate) };
            sqlParameters[4] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestEndDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestEndDate) };
            _dataContext.ExecuteStoredProcedure("USPAssignTestDateToCohort", sqlParameters);
        }

        public void AssignTestToProgram(int programId, int testId, int type, int assetGroupId)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[2] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type };
            sqlParameters[3] = new SqlParameter("@AssetGroupId", SqlDbType.Int, 4) { Value = assetGroupId == 0 ? Convert.DBNull : assetGroupId };
            _dataContext.ExecuteStoredProcedure("USPAssignTestsToProgram", sqlParameters);
        }

        public void AssignTestDateToGroup(GroupTestDates testDate)
        {
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = testDate.Group.GroupId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = testDate.Cohort.CohortId };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testDate.Product.ProductId };
            sqlParameters[3] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = testDate.Type };
            sqlParameters[4] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestStartDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestStartDate) };
            sqlParameters[5] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestEndDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestEndDate) };
            _dataContext.ExecuteStoredProcedure("USPAssignTestDateToGroup", sqlParameters);
        }

        public void AssignTestDateToStudent(StudentTestDates testDate)
        {
            var sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = testDate.Cohort.CohortId };
            sqlParameters[1] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = testDate.Group.GroupId };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testDate.Product.ProductId };
            sqlParameters[3] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = testDate.Student.UserId };
            sqlParameters[4] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = testDate.Type };
            sqlParameters[5] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestStartDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestStartDate) };
            sqlParameters[6] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestEndDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestEndDate) };
            _dataContext.ExecuteStoredProcedure("USPAssignTestDateToStudent", sqlParameters);
        }

        public IEnumerable<ProgramTestDates> GetTestsForProgram(int programId, string searchText)
        {
            var cohortPrograms = new List<ProgramTestDates>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForProgram", sqlParameters))
            {
                while (reader.Read())
                {
                    var assetNameWithProgOfStudy = (reader["TestName"] as string) ?? string.Empty;
                    var assetCategory = (reader["TestCategory"] as string) ?? string.Empty;
                    var progofStudyDisplay = (reader["ProgramofStudyName"] as string) ?? string.Empty;

                    switch (reader["AssetGroupId"].ToInt())
                    {
                        case (int)AssetGroupType.DashboardPN:
                        case (int)AssetGroupType.DashboardRN:
                            assetNameWithProgOfStudy = "Dashboard Links";
                            assetCategory = (reader["AssetGroupName"] as string) ?? string.Empty;
                            break;
                        case (int)AssetGroupType.CaseStudiesRn:
                            assetNameWithProgOfStudy = (reader["AssetGroupName"] as string) ?? string.Empty;
                            assetCategory = (reader["AssetGroupName"] as string) ?? string.Empty;
                            break;                        
                        case (int)AssetGroupType.EssentialNursingSkillsRN:
                        case (int)AssetGroupType.EssentialNursingSkillsPN:
                            progofStudyDisplay = ProgramofStudyType.Both.ToString();
                            break;
                        default:
                            break;
                    }
                    cohortPrograms.Add(new ProgramTestDates
                    {
                        Type = reader["TestType"].ToInt(),
                        Product = new Product { ProductName = assetCategory },
                        Program = new Program
                        {
                            ProgramOfStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                            ProgramOfStudyName = reader["ProgramofStudyName"].ToString() == ProgramofStudyType.RN.ToString() ? string.Empty : progofStudyDisplay,
                        },
                        AssetGroupId = (reader["AssetGroupId"] as int?) ?? 0,
                        TestId = (reader["ProductID"] as int?) ?? 0,
                        TestName = assetNameWithProgOfStudy
                    });
                }
            }

            return cohortPrograms.ToArray();
        }


        public IEnumerable<GroupTestDates> GetTestsForGroup(int programId, int cohortId, int groupId, string searchText)
        {
            var cohortPrograms = new List<GroupTestDates>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[3] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForGroup", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortPrograms.Add(new GroupTestDates
                    {
                        Type = reader["TestType"].ToInt(),
                        Product = new Product { ProductId = (reader["ProductID"] as int?) ?? 0 },
                        TestStartDate = reader["StartDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["StartDate"]),  // (reader["StartDate"] as string) ?? string.Empty,
                        TestEndDate = reader["EndDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["EndDate"]), // (reader["EndDate"] as string) ?? string.Empty,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestId = (reader["ProductID"] as int?) ?? 0,
                        TestType = (reader["TestProductID"] as int?) ?? 0,
                    });
                }
            }

            return cohortPrograms.ToArray();
        }

        public IEnumerable<CohortTestDates> GetTestsForCohort(int programId, int cohortId, int TestId, string searchText)
        {
            var cohortPrograms = new List<CohortTestDates>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForCohort", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortPrograms.Add(new CohortTestDates
                    {
                        Type = reader["AssignType"].ToInt(),
                        Product = new Product { ProductId = (reader["TestType"] as int?) ?? 0 },
                        TestStartDate = reader["StartDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["StartDate"]),  // (reader["StartDate"] as string) ?? string.Empty,
                        TestEndDate = reader["EndDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["EndDate"]), // (reader["EndDate"] as string) ?? string.Empty,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestId = (reader["ProductID"] as int?) ?? 0,
                    });
                }
            }

            return cohortPrograms.ToArray();
        }

        public IEnumerable<StudentTestDates> GetTestsForStudent(int programId, int studentId, int cohortId, int groupId, int TestId, string searchText)
        {
            var studentTestDates = new List<StudentTestDates>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentId };
            sqlParameters[2] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[3] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[4] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForStudent", sqlParameters))
            {
                while (reader.Read())
                {
                    studentTestDates.Add(new StudentTestDates
                    {
                        TestId = (reader["ProductID"] as int?) ?? 0,
                        Type = reader["TestType"].ToInt(),
                        Product = new Product { ProductId = (reader["ProductID"] as int?) ?? 0 },
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestStartDate = reader["StartDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["StartDate"]),
                        TestEndDate = reader["EndDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(reader["EndDate"]),
                        TestType = (reader["TestProductID"] as int?) ?? 0,
                    });
                }
            }

            return studentTestDates.ToArray();
        }

        public int SaveAdmin(Admin admin)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[11];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = admin.UserId };
            sqlParameters[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = admin.UserName };
            sqlParameters[2] = new SqlParameter("@UserPass", SqlDbType.VarChar, 50) { Value = admin.UserPassword };
            sqlParameters[3] = new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = admin.Email };
            sqlParameters[4] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = admin.FirstName };
            sqlParameters[5] = new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = admin.LastName };
            sqlParameters[6] = new SqlParameter("@SecurityLevel", SqlDbType.Int, 4) { Value = admin.SecurityLevel };
            sqlParameters[7] = new SqlParameter("@AdminModifyUser", SqlDbType.Int, 4) { Value = admin.AdminCreateUser };
            sqlParameters[8] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = admin.Institution.InstitutionId };
            sqlParameters[9] = new SqlParameter("@UploadAccess", SqlDbType.Bit, 4) { Value = admin.UploadAccess };
            sqlParameters[10] = new SqlParameter("NewAdminId", SqlDbType.Int, 4);
            sqlParameters[10].Direction = ParameterDirection.Output;

            _dataContext.ExecuteNonQuery("USPSaveAdmin", sqlParameters);
            int newAdminId = (int)sqlParameters[10].Value;
            return newAdminId;
            #endregion
        }

        public void DeleteAdmin(int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPDeleteAdmin", sqlParameters);
        }

        public IEnumerable<Admin> GetAdmins(int userId, string searchString)
        {
            var admins = new List<Admin>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = new SqlParameter("@SearchString", SqlDbType.VarChar, 400) { Value = searchString };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetAdmins", sqlParameters))
            {
                while (reader.Read())
                {
                    admins.Add(new Admin
                    {
                        UserId = (reader["UserId"] as int?) ?? 0,
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                        FirstName = (reader["FirstName"] as string) ?? string.Empty,
                        LastName = (reader["LastName"] as string) ?? string.Empty,
                        SecurityLevel = (reader["SecurityLevel"] as int?) ?? 0,
                        UserPassword = (reader["UserPass"] as string) ?? string.Empty,
                        Email = (reader["Email"] as string) ?? string.Empty,
                        UploadAccess = (bool)(reader["UploadAccess"] is DBNull ? false : reader["UploadAccess"]),
                    });
                }
            }

            return admins.ToArray();
        }

        public IEnumerable<Admin> SearchAdmins(string institutionIds, string securityLevel, string searchString, int programofStudyId)
        {
            var admins = new List<Admin>();
            var spname = programofStudyId == (int)ProgramofStudyType.None ? "USPSearchUnAssignedAdmins" : "USPSearchAdmins";
            var sqlParameters = programofStudyId == (int)ProgramofStudyType.None ? new SqlParameter[1] : new SqlParameter[4];
            #region SqlParameters
            if (programofStudyId == (int)ProgramofStudyType.None)
            {
                sqlParameters[0] = new SqlParameter("@SearchString", SqlDbType.VarChar, 400) { Value = searchString };
            }
            else
            {
                sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 3000) { Value = institutionIds };
                sqlParameters[1] = new SqlParameter("@SecurityLevel", SqlDbType.VarChar, 20) { Value = securityLevel };
                sqlParameters[2] = new SqlParameter("@SearchString", SqlDbType.VarChar, 400) { Value = searchString };
                sqlParameters[3] = new SqlParameter("@programofStudyId", SqlDbType.Int) { Value = programofStudyId };
            }
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader(spname, sqlParameters))
            {
                while (reader.Read())
                {
                    string institutionNameWithProgOfStudy = string.Empty;
                    if (!string.IsNullOrEmpty(reader["InstitutionName"] as string))
                        institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);
                    admins.Add(new Admin
                    {
                        UserId = (reader["UserId"] as int?) ?? 0,
                        FirstName = (reader["FirstName"] as string) ?? string.Empty,
                        LastName = (reader["LastName"] as string) ?? string.Empty,
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                        UserPassword = (reader["UserPass"] as string) ?? string.Empty,
                        Institution = new Institution()
                            {
                                InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                                InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy
                            },
                        AdminType = (reader["AdminType"] as string) ?? string.Empty,
                    });
                }
            }

            return admins.ToArray();
        }

        public void AssignInstitutionsToAdmin(Admin admin, string institutionIds, int programofStudyId)
        {
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@AdminId", SqlDbType.Int, 4) { Value = admin.UserId };
            sqlParameters[1] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = admin.Institution.InstitutionId };
            sqlParameters[2] = new SqlParameter("@Active", SqlDbType.Int, 4) { Value = admin.Institution.Active };
            sqlParameters[3] = new SqlParameter("@AssignedInstitutionIds", SqlDbType.VarChar, 1000) { Value = institutionIds };
            sqlParameters[4] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofStudyId };

            _dataContext.ExecuteStoredProcedure("USPAssignInstitutionsToAdmin", sqlParameters);
        }

        public void AssignStudents(string userId, int cohortId, int groupId, int institutionId)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.VarChar, 4000) { Value = userId };
            sqlParameters[1] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@groupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[3] = new SqlParameter("@institutionId", SqlDbType.Int, 4) { Value = institutionId };
            _dataContext.ExecuteStoredProcedure("USPAssignStudents", sqlParameters);
        }

        public IEnumerable<Admin> AuthenticateUser(string userName, string userPassword)
        {
            var admins = new List<Admin>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 50) { Value = userName };
            sqlParameters[1] = new SqlParameter("@UserPassword", SqlDbType.VarChar, 50) { Value = userPassword };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPAuthenticateAdminUser", sqlParameters))
            {
                while (reader.Read())
                {
                    admins.Add(new Admin
                    {
                        UserId = (reader["UserID"] as int?) ?? 0, // As 0 is used for super admin
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                        FirstName = (reader["FirstName"] as string) ?? string.Empty,
                        LastName = (reader["LastName"] as string) ?? string.Empty,
                        SecurityLevel = (reader["SecurityLevel"] as int?) ?? -1,
                        UserPassword = (reader["UserPass"] as string) ?? string.Empty,
                        Email = (reader["Email"] as string) ?? string.Empty,
                        UploadAccess = (bool)(reader["UploadAccess"] is DBNull ? false : reader["UploadAccess"]),
                    });
                }
            }

            return admins.ToArray();
        }

        public IEnumerable<Institution> GetLocalInstitution(int userId)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLocalInstitution", sqlParameters))
            {
                while (reader.Read())
                {
                    institutions.Add(new Institution
                    {
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                    });
                }
            }

            return institutions;
        }

        public IEnumerable<Email> GetEmail(int emailId)
        {
            var emails = new List<Email>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@EmailId", SqlDbType.Int) { Value = emailId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetEmails", sqlParameters))
            {
                while (reader.Read())
                {
                    emails.Add(new Email
                    {
                        EmailId = (reader["EmailId"] as int?).GetValueOrDefault(0),
                        Body = (reader["Body"] as string) ?? string.Empty,
                        Title = (reader["Title"] as string) ?? string.Empty,
                        EmailType = (reader["EmailType"] as int?).GetValueOrDefault(0)
                    });
                }
            }

            return emails;
        }

        public IEnumerable<Admin> GetAdmin(string institutionIds, string searchCriteria)
        {
            var admins = new List<Admin>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@SearchText", SqlDbType.VarChar) { Value = searchCriteria };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetAdminByInstitution", sqlParameters))
            {
                while (reader.Read())
                {
                    admins.Add(new Admin
                    {
                        UserId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                    });
                }
            }

            return admins;
        }

        public IEnumerable<StudentEntity> SearchStudent(string criteria)
        {
            var students = new List<StudentEntity>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@criteria", SqlDbType.VarChar) { Value = criteria };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchStudentForEmail", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new StudentEntity
                    {
                        StudentId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        StudentName = (reader["UserName"] as string) ?? string.Empty,
                    });
                }
            }

            return students;
        }

        public IEnumerable<Admin> SearchAdmin(string criteria)
        {
            var admins = new List<Admin>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@criteria", SqlDbType.VarChar) { Value = criteria };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchAdmin", sqlParameters))
            {
                while (reader.Read())
                {
                    admins.Add(new Admin
                    {
                        UserId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        UserName = (reader["UserName"] as string) ?? string.Empty,
                    });
                }
            }

            return admins;
        }

        public int CreateCustomEmailToPerson(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string personIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@EmailMissionId", SqlDbType.Int, 4) { Value = 0, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@AdminId", SqlDbType.Int) { Value = adminId };
            sqlParameters[2] = new SqlParameter("@SendTime", SqlDbType.DateTime) { Value = sendTime };
            sqlParameters[3] = new SqlParameter("@EmailId", SqlDbType.Int, 4) { Value = emailId };
            sqlParameters[4] = new SqlParameter("@ToAdminOrStudent", SqlDbType.Bit) { Value = toAdminOrStudent };
            sqlParameters[5] = new SqlParameter("@PersonIds", SqlDbType.VarChar) { Value = personIds };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPCreateCustomEmailToPerson", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public int CreateCustomEmailToGroup(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string groupIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@EmailMissionId", SqlDbType.Int, 4) { Value = 0, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@AdminId", SqlDbType.Int) { Value = adminId };
            sqlParameters[2] = new SqlParameter("@SendTime", SqlDbType.DateTime) { Value = sendTime };
            sqlParameters[3] = new SqlParameter("@EmailId", SqlDbType.Int, 4) { Value = emailId };
            sqlParameters[4] = new SqlParameter("@ToAdminOrStudent", SqlDbType.Bit) { Value = toAdminOrStudent };
            sqlParameters[5] = new SqlParameter("@GroupIds", SqlDbType.VarChar) { Value = groupIds };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPCreateCustomEmailToGroup", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public int CreateCustomEmailToCohort(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string cohortIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@EmailMissionId", SqlDbType.Int, 4) { Value = 0, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@AdminId", SqlDbType.Int) { Value = adminId };
            sqlParameters[2] = new SqlParameter("@SendTime", SqlDbType.DateTime) { Value = sendTime };
            sqlParameters[3] = new SqlParameter("@EmailId", SqlDbType.Int, 4) { Value = emailId };
            sqlParameters[4] = new SqlParameter("@ToAdminOrStudent", SqlDbType.Bit) { Value = toAdminOrStudent };
            sqlParameters[5] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPCreateCustomEmailToCohort", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public int CreateCustomEmailToInstitution(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string institutionIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@EmailMissionId", SqlDbType.Int, 4) { Value = 0, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@AdminId", SqlDbType.Int) { Value = adminId };
            sqlParameters[2] = new SqlParameter("@SendTime", SqlDbType.DateTime) { Value = sendTime };
            sqlParameters[3] = new SqlParameter("@EmailId", SqlDbType.Int, 4) { Value = emailId };
            sqlParameters[4] = new SqlParameter("@ToAdminOrStudent", SqlDbType.Bit) { Value = toAdminOrStudent };
            sqlParameters[5] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            #endregion

            _dataContext.ExecuteStoredProcedure("USPCreateCustomEmailToInstitution", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public IEnumerable<UserTest> GetStudentsForOverRide(int institutionId, string firstName, string lastName, string userName,
            string testName, bool showIncompleteOnly, string cohortIds)
        {
            var tests = new List<UserTest>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar, 8000) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 100) { Value = firstName };
            sqlParameters[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 100) { Value = lastName };
            sqlParameters[4] = new SqlParameter("@UserName", SqlDbType.VarChar, 100) { Value = userName };
            sqlParameters[5] = new SqlParameter("@TestName", SqlDbType.VarChar, 100) { Value = testName };
            sqlParameters[6] = new SqlParameter("@ShowIncompleteOnly", SqlDbType.Bit) { Value = showIncompleteOnly };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudentListForSetOverride", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    tests.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestId"] as int?) ?? 0,
                        TestStatus = (reader["TestStatus"] as int?) ?? 0,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        SuspendQuestionNumber = (reader["SuspendQuestionNumber"] as int?) ?? 0,
                        NumberOfQuestions = (reader["NumberOfQuestions"] as int?) ?? 0,
                        TimeRemaining = reader["TimeRemaining"] as string ?? string.Empty,
                        Student = new Student()
                        {
                            UserName = reader["UserName"] as string ?? string.Empty,
                            FirstName = reader["FirstName"] as string ?? string.Empty,
                            LastName = reader["LastName"] as string ?? string.Empty
                        },
                        Test = new Test()
                        {
                            TestName = reader["TestName"] as string ?? string.Empty,
                            ProductId = (reader["ProductID"] as int?) ?? 0,
                            Question = new Question()
                            {
                                AnswserTrack = reader["AnswerTrack"] as string ?? string.Empty,
                                OrderedIndexes = reader["OrderedIndexes"] as string ?? string.Empty,
                            }
                        },
                        TestCompleted = reader["TestCompleted"] as DateTime?,
                        TimeUsed = reader["TimeUsed"] as string ?? string.Empty,
                        AnsweredCount = (reader["AnsweredCount"] as int?) ?? 0,
                        LastQuestionAnswer = (reader["LastQuestionAnswer"] as int?) ?? 0,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        CohortName = reader["CohortName"] as string ?? string.Empty
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<UserTest> GetDeletedTestListForStudents(int institutionId, string firstName, string lastName, string userName,
            string testName, bool showIncompleteOnly, string cohortIds)
        {
            var tests = new List<UserTest>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar, 8000) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 100) { Value = firstName };
            sqlParameters[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 100) { Value = lastName };
            sqlParameters[4] = new SqlParameter("@UserName", SqlDbType.VarChar, 100) { Value = userName };
            sqlParameters[5] = new SqlParameter("@TestName", SqlDbType.VarChar, 100) { Value = testName };
            sqlParameters[6] = new SqlParameter("@ShowIncompleteOnly", SqlDbType.Bit) { Value = showIncompleteOnly };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetDeletedTestListForStudents", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);

                    tests.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestId"] as int?) ?? 0,
                        TestStatus = (reader["TestStatus"] as int?) ?? 0,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        Student = new Student()
                        {
                            UserName = reader["UserName"] as string ?? string.Empty,
                            FirstName = reader["FirstName"] as string ?? string.Empty,
                            LastName = reader["LastName"] as string ?? string.Empty
                        },
                        Test = new Test()
                        {
                            TestName = reader["TestName"] as string ?? string.Empty,
                        },
                        UserName = reader["DeletedBy"] as string ?? string.Empty,
                        TestDeletedDate = (reader["DeletedDate"] as DateTime?) ?? DateTime.MinValue,
                        TestCompleted = reader["TestCompleted"] as DateTime?,
                        TimeUsed = reader["TimeUsed"] as string ?? string.Empty,
                        AnsweredCount = (reader["AnsweredCount"] as int?) ?? 0,
                        NumberOfQuestions = (reader["NumberOfQuestions"] as int?) ?? 0,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        CohortName = reader["CohortName"] as string ?? string.Empty
                    });
                }
            }

            return tests.ToArray();
        }

        public void DeleteTest(int testId, string userName)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@TestId", SqlDbType.Int, 7) { Value = testId };
            sqlParameters[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 4000) { Value = userName };

            _dataContext.ExecuteStoredProcedure("USPDeleteTest", sqlParameters);
        }

        public void ResumeTest(string userTestId, string userName)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserTestId", SqlDbType.VarChar, 8000) { Value = userTestId };
            sqlParameters[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 4000) { Value = userName };

            _dataContext.ExecuteStoredProcedure("uspResumeTest", sqlParameters);
        }

        public void SaveEmail(int emailId, string title, string body)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@emailId", SqlDbType.Int) { Value = emailId };
            sqlParameters[1] = new SqlParameter("@title", SqlDbType.VarChar) { Value = title };
            sqlParameters[2] = new SqlParameter("@body", SqlDbType.VarChar) { Value = body };

            _dataContext.ExecuteStoredProcedure("USPSaveEmail", sqlParameters);
        }

        public IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules()
        {
            IDictionary<UserType, IDictionary<Module, IList<UserAction>>> rules = new Dictionary<UserType, IDictionary<Module, IList<UserAction>>>();

            using (IDataReader reader = _dataContext.GetDataReader("USPGetAdminAuthorizationRules"))
            {
                while (reader.Read())
                {
                    // Since there are more rows in DB than User Types that are in enum, we need this check to exclude those rows.
                    if (false == "0,1,2,3,5".Split(',').Contains(reader["SecurityLevel"].ToString()))
                    {
                        continue;
                    }

                    IDictionary<Module, IList<UserAction>> userTypeRules = new Dictionary<Module, IList<UserAction>>();

                    IList<UserAction> actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["I_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["I_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["I_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    userTypeRules.Add(Module.Institution, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["C_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["C_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["C_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    if (IsPermissionEnabled(reader["C_AccessDatesEdit"]))
                    {
                        actions.Add(UserAction.AccessDatesEdit);
                    }

                    if (IsPermissionEnabled(reader["C_AssisgnStudents"]))
                    {
                        actions.Add(UserAction.AssignStudents);
                    }

                    if (IsPermissionEnabled(reader["C_EditTestDates"]))
                    {
                        actions.Add(UserAction.EditTestDates);
                    }

                    if (IsPermissionEnabled(reader["C_AssignProgram"]))
                    {
                        actions.Add(UserAction.AssignProgram);
                    }

                    userTypeRules.Add(Module.Cohort, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["G_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["G_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["G_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    if (IsPermissionEnabled(reader["G_EditTestDates"]))
                    {
                        actions.Add(UserAction.EditTestDates);
                    }

                    if (IsPermissionEnabled(reader["G_AssignStudents"]))
                    {
                        actions.Add(UserAction.AssignStudents);
                    }

                    userTypeRules.Add(Module.Group, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["S_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["S_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["S_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    if (IsPermissionEnabled(reader["S_AssignToCohort"]))
                    {
                        actions.Add(UserAction.AssignToCohort);
                    }

                    if (IsPermissionEnabled(reader["S_AssignToGroup"]))
                    {
                        actions.Add(UserAction.AssignToGroup);
                    }

                    if (IsPermissionEnabled(reader["S_EditTestDates"]))
                    {
                        actions.Add(UserAction.EditTestDates);
                    }

                    userTypeRules.Add(Module.Student, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["A_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["A_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["A_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    userTypeRules.Add(Module.UserManagement, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["P_Add"]))
                    {
                        actions.Add(UserAction.Add);
                    }

                    if (IsPermissionEnabled(reader["P_Edit"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    if (IsPermissionEnabled(reader["P_Delete"]))
                    {
                        actions.Add(UserAction.Delete);
                    }

                    if (IsPermissionEnabled(reader["P_AssignTests"]))
                    {
                        actions.Add(UserAction.AssignTests);
                    }

                    userTypeRules.Add(Module.Program, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["R_InstitutionResults"]))
                    {
                        actions.Add(UserAction.InstitutionResults);
                    }

                    if (IsPermissionEnabled(reader["R_CohortResults"]))
                    {
                        actions.Add(UserAction.CohortResults);
                    }

                    if (IsPermissionEnabled(reader["R_GroupResults"]))
                    {
                        actions.Add(UserAction.GroupResults);
                    }

                    if (IsPermissionEnabled(reader["P_AssignTests"]))
                    {
                        actions.Add(UserAction.AssignTests);
                    }

                    if (IsPermissionEnabled(reader["R_StudentResults"]))
                    {
                        actions.Add(UserAction.StudentResults);
                    }

                    if (IsPermissionEnabled(reader["R_KaplanReport"]))
                    {
                        actions.Add(UserAction.KaplanReport);
                    }

                    userTypeRules.Add(Module.Reports, actions);

                    actions = new List<UserAction>();
                    if (IsPermissionEnabled(reader["Cms"]))
                    {
                        actions.Add(UserAction.Edit);
                    }

                    userTypeRules.Add(Module.CMS, actions);

                    rules.Add(EnumHelper.GetUserType(reader["SecurityLevel"].ToInt()), userTypeRules);
                }
            }

            return rules;
        }

        public int UserLogIn(string username, string password)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserName", SqlDbType.VarChar) { Value = username };
            sqlParameters[1] = new SqlParameter("@UserPass", SqlDbType.VarChar) { Value = password };
            #endregion

            _dataContext.ExecuteStoredProcedure("ReturnUserID", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public IEnumerable<Student> GetUserInfo(int UserID)
        {
            var userInfo = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.VarChar) { Value = UserID };
            #endregion
            /*ReturnUserInfoByUserID does not exists*/
            using (IDataReader reader = _dataContext.GetDataReader("ReturnUserInfoByUserID", sqlParameters))
            {
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(reader["CohortID"].ToString()))
                    {
                        throw new Exception("Student has not been assigned into any Cohort. Please contact KAPLAN at 1 (800) 533 - 8850");
                    }
                    else if (string.IsNullOrEmpty(reader["GroupID"].ToString()))
                    {
                        throw new Exception("You are not assigned into any Group. Please contact KAPLAN at 1 (800) 533 - 8850");
                    }
                    else
                    {
                        userInfo.Add(new Student
                        {
                            InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                            ProgramId = (reader["ProgramID"] as int?) ?? 0,
                            CohortId = (reader["CohortID"] as int?) ?? 0,
                            GroupId = (reader["GroupID"] as int?) ?? 0,
                            FirstName = (reader["FirstName"] as String) ?? string.Empty,
                            LastName = (reader["LastName"] as String) ?? string.Empty,
                            UserName = (reader["UserName"] as String) ?? string.Empty,
                            Hour = (reader["Hour"] as int?) ?? 0,
                        });
                    }
                }

                return userInfo;
            }
        }

        public int GetUserID()
        {
            return (_dataContext.ExecuteScalar("uspGetUserID") as int?) ?? 0;
        }

        public int GetInstitutionIDByFacilityID(int FacilityID)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@FID", SqlDbType.VarChar, 50) { Value = FacilityID };
            #endregion
            return (_dataContext.ExecuteScalar("uspGetInstitutionIDByFacilityID", sqlParameters) as int?) ?? 0;
        }

        public object GetUser(string UserId, int institutionId)
        {
            List<int> users = new List<int>();

            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 50) { Value = UserId };
            sqlParameters[1] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = institutionId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetUser", sqlParameters))
            {
                while (reader.Read())
                {
                    if (reader["UserID"] != null)
                    {
                        users.Add(Convert.ToInt32(reader["UserID"]));
                    }
                }
            }

            return users.FirstOrDefault();
        }

        public string GetPassword()
        {
            string vAdj = string.Empty;
            string vNoun = string.Empty;
            using (IDataReader reader = _dataContext.GetDataReader("uspGetNounAdjForPassword"))
            {
                while (reader.Read())
                {
                    vAdj = (reader["Adj"] as string) ?? string.Empty;
                }
            }

            using (IDataReader reader = _dataContext.GetDataReader("uspGetNounAdjForPassword"))
            {
                while (reader.Read())
                {
                    vNoun = (reader["Noun"] as string) ?? string.Empty;
                }
            }

            string pass = vAdj + "-" + vNoun;
            return pass;
        }

        public bool GetUpdatedIntegratedUser(int UserID, string ClassCode)
        {
            string CohortIDA = string.Empty;
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            if (ClassCode != null)
            {
                sqlParameters[0] = new SqlParameter("@ClassCode", SqlDbType.VarChar) { Value = ClassCode };
            }
            #endregion

            CohortIDA = _dataContext.ExecuteScalar("uspGetCohortIdByCohortDesc", sqlParameters).ToString();

            var sqlParametersForUpdate = new SqlParameter[2];
            sqlParametersForUpdate[0] = new SqlParameter("@StudentID", SqlDbType.Int) { Value = UserID };
            sqlParametersForUpdate[1] = new SqlParameter("@CohortID", SqlDbType.Int) { Value = Convert.ToInt32(CohortIDA) };

            return _dataContext.ExecuteNonQuery("uspUpdateIntegratedusers", sqlParametersForUpdate) > 0;
        }

        public void DeleteInstitution(int institutionId, int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@DeleteUserId", SqlDbType.Int) { Value = userId };
            #endregion
            _dataContext.ExecuteNonQuery("uspDeleteInstitution", sqlParameters);
        }

        public void SaveHelpfulDocuments(HelpfulDocument helpfulDocument)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[11];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = helpfulDocument.Id, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 100) { Value = helpfulDocument.FileName };
            sqlParameters[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 100) { Value = helpfulDocument.Title };
            sqlParameters[3] = new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = helpfulDocument.Type };
            sqlParameters[4] = new SqlParameter("@Size", SqlDbType.Float, 0) { Value = helpfulDocument.Size };
            sqlParameters[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 4) { Value = helpfulDocument.CreatedDateTime };
            sqlParameters[6] = new SqlParameter("@Description", SqlDbType.NVarChar, 1000) { Value = helpfulDocument.Description };
            sqlParameters[7] = new SqlParameter("@Status", SqlDbType.Int, 4) { Value = helpfulDocument.Status };
            sqlParameters[8] = new SqlParameter("@GUID", SqlDbType.NVarChar, 100) { Value = helpfulDocument.GUID };
            sqlParameters[9] = new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = helpfulDocument.CreatedBy };
            sqlParameters[10] = new SqlParameter("@IsLink", SqlDbType.Bit) { Value = helpfulDocument.IsLink };
            #endregion

            _dataContext.ExecuteStoredProcedure("uspSaveHelpfulDocuments", sqlParameters);
            helpfulDocument.Id = (int)sqlParameters[0].Value;
        }

        public IEnumerable<HelpfulDocument> GetHelpfulDocuments(int documentId, string GUID)
        {
            var sqlParameters = new SqlParameter[2];
            var helpfulDocuments = new List<HelpfulDocument>();
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = documentId };
            sqlParameters[1] = new SqlParameter("@GUID", SqlDbType.NVarChar, 100) { Value = GUID };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetHelpfulDocuments", sqlParameters))
            {
                while (reader.Read())
                {
                    helpfulDocuments.Add(new HelpfulDocument
                    {
                        Id = (reader["Id"] as int?) ?? 0,
                        Title = (reader["Title"] as string) ?? string.Empty,
                        Description = (reader["Description"] as string) ?? string.Empty,
                        FileName = (reader["FileName"] as string) ?? string.Empty,
                        Type = (reader["Type"] as string) ?? string.Empty,
                        Size = (reader["Size"] as double?) ?? 0,
                        Status = (reader["Status"] as int?) ?? 0,
                        CreatedDateTime = (reader["CreatedDate"] as DateTime?) ?? null,
                        GUID = (reader["GUID"] as string) ?? string.Empty,
                        CreatedBy = (reader["CreatedBy"] as int?) ?? 0,
                        IsLink = (bool)(reader["IsLink"] is DBNull ? false : reader["IsLink"]),
                        Admin = new Admin()
                        {
                            FirstName = (reader["FirstName"] as string) ?? string.Empty,
                            LastName = (reader["LastName"] as string) ?? string.Empty
                        }
                    });
                }
            }

            return helpfulDocuments.ToArray();
        }

        public IEnumerable<HelpfulDocument> SearchHelpfulDocs(string searchKeyword, bool IsLink)
        {
            var docInfo = new List<HelpfulDocument>();

            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@SearchKeyword", SqlDbType.VarChar, 200) { Value = searchKeyword };
            sqlParameters[1] = new SqlParameter("@Status", SqlDbType.Int, 4) { Value = 1 };
            sqlParameters[2] = new SqlParameter("@IsLink", SqlDbType.Bit) { Value = IsLink };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspSearchHelpfulDocs", sqlParameters))
            {
                while (reader.Read())
                {
                    docInfo.Add(new HelpfulDocument
                    {
                        Id = (reader["Id"] as int?) ?? 0,
                        FileName = (reader["FileName"] as string) ?? string.Empty,
                        Title = (reader["Title"] as string) ?? string.Empty,
                        Type = (reader["Type"] as string) ?? string.Empty,
                        Size = Convert.ToDouble(reader["Size"]),
                        CreatedDateTime = (reader["CreatedDate"] as DateTime?) ?? null,
                        Status = (reader["Status"] as int?) ?? 0,
                        Description = (reader["Description"] as string) ?? string.Empty,
                        GUID = (reader["GUID"] as string) ?? string.Empty,
                        CreatedBy = (reader["CreatedBy"] as int?) ?? 0,
                        IsLink = (bool)(reader["IsLink"] is DBNull ? false : reader["IsLink"]),
                        Admin = new Admin()
                        {
                            FirstName = (reader["FirstName"] as string) ?? string.Empty,
                            LastName = (reader["LastName"] as string) ?? string.Empty
                        }
                    });
                }
            }

            return docInfo;
        }

        public void DeleteHelpfulDoc(int userId, int docId)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 5) { Value = docId };
            sqlParameters[1] = new SqlParameter("@UserId", SqlDbType.Int, 10) { Value = userId };

            _dataContext.ExecuteStoredProcedure("uspDeleteHelpfulDoc", sqlParameters);
        }

        public IEnumerable<Country> GetCountries(int countryId)
        {
            var countries = new List<Country>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CountryId", SqlDbType.Int, 4) { Value = countryId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetCountries", sqlParameters))
            {
                while (reader.Read())
                {
                    countries.Add(new Country
                    {
                        CountryId = reader["CountryID"].ToInt(),
                        CountryName = (reader["CountryName"] as string) ?? string.Empty,
                        Status = (reader["Status"] as int?) ?? 0
                    });
                }
            }

            return countries.ToArray();
        }

        public IEnumerable<State> GetStates(int countryId, int stateId)
        {
            var states = new List<State>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CountryId", SqlDbType.Int) { Value = countryId };
            sqlParameters[1] = new SqlParameter("@StateId", SqlDbType.Int) { Value = stateId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetStates", sqlParameters))
            {
                while (reader.Read())
                {
                    states.Add(new State
                    {
                        StateId = (reader["StateID"] as int?) ?? 0,
                        CountryId = (reader["CountryID"] as int?) ?? 0,
                        StateName = (reader["StateName"] as string) ?? string.Empty
                    });
                }
            }

            return states.ToArray();
        }

        public int SaveAddress(Address address)
        {
            var _addressId = 0;
            #region SqlParameters
            var sqlParameters = new SqlParameter[9];
            sqlParameters[0] = new SqlParameter("@AddressId", SqlDbType.Int) { Value = address.AddressId };
            sqlParameters[1] = new SqlParameter("@Address1", SqlDbType.VarChar, 50) { Value = address.Address1 };
            sqlParameters[2] = new SqlParameter("@Address2", SqlDbType.VarChar, 50) { Value = address.Address2 };
            sqlParameters[3] = new SqlParameter("@Address3", SqlDbType.VarChar, 50) { Value = address.Address3 };
            sqlParameters[4] = new SqlParameter("@CountryId", SqlDbType.Int) { Value = address.AddressCountry.CountryId };
            sqlParameters[5] = new SqlParameter("@StateName", SqlDbType.VarChar, 100) { Value = address.AddressState.StateName };
            sqlParameters[6] = new SqlParameter("@Zip", SqlDbType.VarChar, 100) { Value = address.Zip };
            sqlParameters[7] = new SqlParameter("@Status", SqlDbType.SmallInt) { Value = address.Status };
            sqlParameters[8] = new SqlParameter("@NewAddressId", SqlDbType.Int, 4);
            sqlParameters[8].Direction = ParameterDirection.Output;
            #endregion
            _dataContext.ExecuteNonQuery("uspSaveAddress", sqlParameters);
            _addressId = (int)sqlParameters[8].Value;
            return _addressId;
        }

        public Address GetAddress(int addressId)
        {
            var address = new Address();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AddressId", SqlDbType.Int) { Value = addressId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAddress", sqlParameters))
            {
                if (reader.Read())
                {
                    address.AddressId = (reader["AddressID"] as int?) ?? 0;
                    address.Address1 = (reader["Address1"] as string) ?? string.Empty;
                    address.Address2 = (reader["Address2"] as string) ?? string.Empty;
                    address.Address3 = (reader["Address3"] as string) ?? string.Empty;
                    address.AddressCountry = new Country { CountryName = (reader["CountryName"] as string) ?? string.Empty, CountryId = (reader["CountryID"] as Int16?) ?? 0 };
                    address.AddressState = new State { StateName = (reader["StateName"] as string) ?? string.Empty };
                    address.Zip = (reader["Zip"] as string) ?? string.Empty;
                    address.Status = (reader["Status"] as int?) ?? 0;
                }
            }

            return address;
        }

        public IEnumerable<InstitutionContact> GetInstitutionContacts(int institutionId, int contactId)
        {
            var institutions = new List<InstitutionContact>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@ContactId", SqlDbType.Int) { Value = contactId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetInstitutionContacts", sqlParameters))
            {
                while (reader.Read())
                {
                    institutions.Add(new InstitutionContact
                    {
                        ContactId = (reader["ContactId"] as int?) ?? 0,
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        ContactType = (reader["ContactType"] as Int16?) ?? 0,
                        Name = (reader["Name"] as string) ?? string.Empty,
                        PhoneNumber = (reader["PhoneNumber"] as string) ?? string.Empty,
                        ContactEmail = (reader["Email"] as string) ?? string.Empty,
                        Status = (reader["Status"] as int?) ?? 0,
                        RecordSortOrder = (reader["SortOrder"] as Int16?) ?? 0,
                    });
                }
            }

            return institutions.ToArray();
        }

        public void SaveInstitutionContact(InstitutionContact institutionConatct)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[11];
            sqlParameters[0] = new SqlParameter("@ContactId", SqlDbType.Int) { Value = institutionConatct.ContactId, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = institutionConatct.InstitutionId };
            sqlParameters[2] = new SqlParameter("@ContactType", SqlDbType.SmallInt) { Value = institutionConatct.ContactType };
            sqlParameters[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = institutionConatct.Name };
            sqlParameters[4] = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 50) { Value = institutionConatct.PhoneNumber };
            sqlParameters[5] = new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = institutionConatct.ContactEmail };
            sqlParameters[6] = new SqlParameter("@Status", SqlDbType.Int) { Value = institutionConatct.Status };
            sqlParameters[7] = new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = institutionConatct.CreatedBy };
            sqlParameters[8] = new SqlParameter("@CreatedDate", SqlDbType.SmallDateTime) { Value = institutionConatct.CreatedDate ?? Convert.DBNull };
            sqlParameters[9] = new SqlParameter("@DeletedBy", SqlDbType.Int) { Value = institutionConatct.DeletedBy ?? Convert.DBNull };
            sqlParameters[10] = new SqlParameter("@DeletedDate", SqlDbType.SmallDateTime) { Value = institutionConatct.DeletedDate ?? Convert.DBNull };
            #endregion
            _dataContext.ExecuteStoredProcedure("uspSaveInstitutionContact", sqlParameters);
            institutionConatct.ContactId = (int)sqlParameters[0].Value;
        }

        public void SaveAdhocGroup(AdhocGroup adhocGroup)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@AdhocGroupID", SqlDbType.Int, 4) { Value = adhocGroup.AdhocGroupId, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@AdhocGroupName", SqlDbType.VarChar, 50) { Value = adhocGroup.AdhocGroupName ?? Convert.DBNull };
            sqlParameters[2] = new SqlParameter("@IsADAGroup", SqlDbType.Bit) { Value = adhocGroup.IsAdaGroup };
            sqlParameters[3] = new SqlParameter("@ADA", SqlDbType.Bit) { Value = adhocGroup.ADA ?? Convert.DBNull };
            sqlParameters[4] = new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = adhocGroup.CreatedBy };
            sqlParameters[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value = DateTime.Now };
            #endregion

            _dataContext.ExecuteStoredProcedure("uspSaveAdhocGroup", sqlParameters);
            adhocGroup.AdhocGroupId = (int)sqlParameters[0].Value;
        }

        public void SaveAdhocGroupStudent(int studentId, int adhocGroupId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@AdhocGroupID", SqlDbType.Int, 4) { Value = adhocGroupId };
            sqlParameters[1] = new SqlParameter("@StudentID", SqlDbType.Int, 4) { Value = studentId };
            #endregion

            _dataContext.ExecuteStoredProcedure("uspSaveAdhocGroupStudent", sqlParameters);
        }

        public IEnumerable<Student> SearchStudentForTest(int institutionId, int cohortId, int groupId, string searchString)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@institutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@groupId", SqlDbType.Int, 4) { Value = groupId };
            sqlParameters[3] = new SqlParameter("@searchString", SqlDbType.VarChar, 100) { Value = searchString };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspSearchStudentForTest", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        Cohort = new Cohort() { CohortName = (reader["CohortName"] as string) ?? string.Empty, CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution() { InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty, InstitutionId = (reader["InstitutionID"] as int?) ?? 0 },
                        Group = new Group() { GroupName = (reader["GroupName"] as string) ?? string.Empty, GroupId = (reader["GroupId"] as int?) ?? 0 },
                    });
                }
            }

            return students;
        }

        public void UpdateStudentsADA(string students, bool Ada)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Students", SqlDbType.VarChar, 2000) { Value = students };
            sqlParameters[1] = new SqlParameter("@Ada", SqlDbType.Bit) { Value = Ada };
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateStudentsADA", sqlParameters);
        }

        public IEnumerable<AdhocGroupTestDetails> GetAdhocGroupTests(int adhocGroupId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AdhocGroupId", SqlDbType.Int, 4) { Value = adhocGroupId };

            var adhocGroupTestDetails = new List<AdhocGroupTestDetails>();

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAdhocGroupTests", sqlParameters))
            {
                while (reader.Read())
                {
                    adhocGroupTestDetails.Add(new AdhocGroupTestDetails
                    {
                        AdhocGroupTestDetailId = (reader["AdhocGroupTestDetailId"] as int?) ?? 0,
                        AdhocGroupId = (reader["AdhocGroupId"] as int?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        CreatedBy = (reader["CreatedBy"] as int?) ?? 0,
                        CreatedDate = (reader["CreatedDate"] as DateTime?) ?? null,
                        StartDate = Convert.ToString((reader["StartDate"] as DateTime?) ?? null),
                        EndDate = Convert.ToString((reader["EndDate"] as DateTime?) ?? null),
                        Test = new Test()
                        {
                            TestName = reader["TestName"] as string ?? string.Empty,
                        }
                    });
                }
            }

            return adhocGroupTestDetails;
        }

        public void SaveAdhocGroupTest(AdhocGroupTestDetails adhocGroupTestDetails)
        {
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@TestID", SqlDbType.Int, 4) { Value = adhocGroupTestDetails.TestId };
            sqlParameters[1] = new SqlParameter("@AdhocGroupID", SqlDbType.Int, 4) { Value = adhocGroupTestDetails.AdhocGroupId };
            sqlParameters[2] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(adhocGroupTestDetails.StartDate) ? Convert.DBNull : Convert.ToDateTime(adhocGroupTestDetails.StartDate) };
            sqlParameters[3] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(adhocGroupTestDetails.EndDate) ? Convert.DBNull : Convert.ToDateTime(adhocGroupTestDetails.EndDate) };
            sqlParameters[4] = new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = adhocGroupTestDetails.CreatedBy };
            sqlParameters[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value = adhocGroupTestDetails.CreatedDate };
            _dataContext.ExecuteStoredProcedure("uspSaveAdhocGroupTestDate", sqlParameters);
        }

        public IEnumerable<Student> GetAdhocGroupStudentDetail(int adhocGroupId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AdhocGroupId", SqlDbType.Int, 4) { Value = adhocGroupId };
            var students = new List<Student>();

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAdhocGroupStudentDetail", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["StudentId"] as int?) ?? 0,
                        CohortId = (reader["CohortId"] as int?) ?? 0,
                        GroupId = (reader["GroupId"] as int?) ?? 0,
                    });
                }
            }

            return students.ToArray();
        }

        public void AssignAdhocTests(StudentTestDates studentTestDates)
        {
            var sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = studentTestDates.Cohort.CohortId };
            sqlParameters[1] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = studentTestDates.Group.GroupId };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = studentTestDates.Product.ProductId };
            sqlParameters[3] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentTestDates.Student.UserId };
            sqlParameters[4] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = studentTestDates.Type };
            sqlParameters[5] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = studentTestDates.TestStartDate };
            sqlParameters[6] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = studentTestDates.TestEndDate };
            _dataContext.ExecuteStoredProcedure("uspAssignAdhocStudentTestDates", sqlParameters);
        }

        public IEnumerable<Test> GetTestsByCohort(int programId, int cohortId, int TestId, string searchText)
        {
            var cohortPrograms = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[2] = new SqlParameter("@SearchString", SqlDbType.VarChar, 1000) { Value = searchText };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForCohort", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortPrograms.Add(new Test
                    {
                        Product = new Product { ProductId = (reader["TestType"] as int?) ?? 0 },
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestId = (reader["ProductID"] as int?) ?? 0,
                        Type = (reader["AssignType"] as string) ?? "0",
                    });
                }
            }

            return cohortPrograms.ToArray();
        }

        public IDictionary<int, string> CheckSystem(bool isProductionApp)
        {
            IDictionary<int, string> returnValues = new Dictionary<int, string>();

            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@IsProductionApp", SqlDbType.Bit, 1) { Value = isProductionApp };
            sqlParameters[1] = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 200);
            sqlParameters[1].Direction = ParameterDirection.Output;
            #endregion

            _dataContext.ExecuteNonQuery("uspCheckSystem", sqlParameters);
            returnValues.Add(1, sqlParameters[1].Value.ToString());

            //// To be changed later to return tabular data
            ////using (IDataReader reader = _dataContext.GetDataReader("uspCheckSystem", sqlParameters))
            ////{
            ////    while (reader.Read())
            ////    {
            ////        // returnValues.Add((reader["TestName"] as int?) ?? 0, (reader["TestName"] as string) ?? "");
            ////    }
            ////}

            return returnValues;
        }

        public List<Student> GetAssignedStudentforGroup(GroupTestDates testDate)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@GroupId", SqlDbType.Int, 4) { Value = testDate.Group.GroupId };
            sqlParameters[1] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = testDate.Cohort.CohortId };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testDate.Product.ProductId };
            sqlParameters[3] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestStartDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestStartDate) };
            sqlParameters[4] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestEndDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestEndDate) };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAssignedStudentforGroup", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        Test = new Test()
                        {
                            TestName = (reader["TestName"] as string) ?? string.Empty,
                            TestId = (reader["TestId"] as int?) ?? 0
                        }
                    });
                }
            }

            return students;
        }

        public List<Student> GetAssignedStudentforCohort(CohortTestDates testDate)
        {
            var students = new List<Student>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = testDate.Cohort.CohortId };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testDate.Product.ProductId };
            sqlParameters[2] = new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestStartDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestStartDate) };
            sqlParameters[3] = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = String.IsNullOrEmpty(testDate.TestEndDate) ? Convert.DBNull : Convert.ToDateTime(testDate.TestEndDate) };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAssignedStudentforCohort", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        Test = new Test()
                        {
                            TestName = (reader["TestName"] as string) ?? string.Empty,
                            TestId = (reader["TestId"] as int?) ?? 0
                        }
                    });
                }
            }

            return students;
        }

        public List<EmailMission> GetStudentEmailMission(string userIds, string groupIds, string cohortIds, string institutionIds)
        {
            var emailMissions = new List<EmailMission>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@UserIds", SqlDbType.VarChar) { Value = userIds };
            sqlParameters[1] = new SqlParameter("@GroupIds", SqlDbType.VarChar) { Value = groupIds };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[3] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetStudentMissionRecipientMailIdDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    emailMissions.Add(new EmailMission
                    {
                        EmailId = reader["Email"] as string ?? string.Empty,
                        UserId = (reader["UserId"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty
                    });
                }
            }

            return emailMissions;
        }

        public List<EmailMission> GetAdminEmailMission(string userIds, string institutionIds)
        {
            var emailMissions = new List<EmailMission>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserIds", SqlDbType.VarChar) { Value = userIds };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAdminMissionRecipientsEmailIdDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    emailMissions.Add(new EmailMission
                    {
                        EmailId = reader["Email"] as string ?? string.Empty,
                        UserId = (reader["UserId"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty
                    });
                }
            }

            return emailMissions;
        }

        public LoginContent GetLoginContent(int contentId)
        {
            var sqlParameters = new SqlParameter[1];
            var LoginContent = new LoginContent();
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.NVarChar) { Value = contentId };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetLoginContentById", sqlParameters))
            {
                while (reader.Read())
                {
                    LoginContent.Id = (reader["Id"] as int?) ?? 0;
                    LoginContent.Content = (reader["Content"] as string) ?? string.Empty;
                    LoginContent.ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty;
                    LoginContent.ReleasedContent = (reader["ReleasedContent"] as string) ?? string.Empty;
                }
            }

            return LoginContent;
        }

        public List<Program> GetBulkProgramDetails(int testId, string type, int programOfStudyId)
        {
            var Programs = new List<Program>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@TestId", SqlDbType.Int) { Value = testId };
            sqlParameters[1] = new SqlParameter("@Type", SqlDbType.VarChar) { Value = type };
            sqlParameters[2] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programOfStudyId };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestPrograms", sqlParameters))
            {
                while (reader.Read())
                {
                    Programs.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        ProgramName = reader["ProgramName"] as string ?? string.Empty,
                        Description = reader["Description"] as string ?? string.Empty,
                        IsTestAssignedToProgram = Convert.ToBoolean(reader["IsTestAssignedToProgram"]),
                        ProgramOfStudyName = reader["ProgramofStudyName"] as string ?? string.Empty
                    });
                }
            }

            return Programs;
        }

        public void SaveBulkModifiedPrograms(int testId, int type, string programIds)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ProgramIds", SqlDbType.VarChar) { Value = programIds };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int) { Value = testId };
            sqlParameters[2] = new SqlParameter("@Type", SqlDbType.Int) { Value = type };

            _dataContext.ExecuteStoredProcedure("Uspsavebulkmodifiedprograms", sqlParameters);
        }

        /// <summary>
        /// Gets the Program of Studies.
        /// </summary>
        /// <returns>ProgramofStudies list</returns>
        public IEnumerable<ProgramofStudy> GetProgramofStudies()
        {
            var programofStudies = new List<ProgramofStudy>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetProgramofStudies"))
            {
                while (reader.Read())
                {
                    programofStudies.Add(new ProgramofStudy
                    {
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty,
                    });
                }
            }

            return programofStudies;
        }

        private bool IsPermissionEnabled(object rule)
        {
            return (rule.ToInt() == 1);
        }

        public IEnumerable<AuditTrail> GetAuditTrail(int studentId)
        {
            var auditTrails = new List<AuditTrail>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAuditTrailsByStudentID", sqlParameters))
            {
                while (reader.Read())
                {
                    auditTrails.Add(new AuditTrail()
                    {
                        AuditTrailId = (reader["AuditTrailId"] as int?) ?? 0,
                        StudentId = (reader["StudentId"] as int?) ?? 0,
                        StudentUserName = reader["StudentUserName"] as string ?? string.Empty,
                        FromInstitution = reader["FromInstitution"] as string ?? string.Empty,
                        FromCohort = reader["FromCohort"] as string ?? string.Empty,
                        FromGroup = reader["FromGroup"] as string ?? string.Empty,
                        ToInstitution = reader["ToInstitution"] as string ?? string.Empty,
                        ToCohort = reader["ToCohort"] as string ?? string.Empty,
                        ToGroup = reader["ToGroup"] as string ?? string.Empty,
                        DateMoved = (DateTime?)(reader["DateMoved"] is DBNull ? null : reader["DateMoved"]),
                        AdminUserId = (reader["AdminUserId"] as int?) ?? 0,
                        AdminUserName = reader["AdminUserName"] as string ?? string.Empty,
                    });
                }
            }

            return auditTrails;
        }

        public IEnumerable<AssetGroup> GetAssetGroups(int programofStudyId)
        {
            var assetGroups = new List<AssetGroup>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofStudyId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAssetGroups", sqlParameters))
            {
                while (reader.Read())
                {
                    assetGroups.Add(new AssetGroup()
                    {
                        AssetGroupId = (reader["AssetGroupId"] as int?) ?? 0,
                        AssetGroupName = reader["AssetGroupName"] as string ?? string.Empty,
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                        ProductId = (reader["ProductId"] as int?) ?? 0,
                    });
                }
            }
            return assetGroups;
        }

        public IEnumerable<Asset> GetAssets(int assetGroupId)
        {
            var assets = new List<Asset>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AssetGroupId", SqlDbType.Int, 4) { Value = assetGroupId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetAssets", sqlParameters))
            {
                while (reader.Read())
                {
                    assets.Add(new Asset()
                    {
                        AssetId = (reader["AssetId"] as int?) ?? 0,
                        AssetName = reader["AssetName"] as string ?? string.Empty,
                        AssetGroupId = (reader["AssetGroupId"] as int?) ?? 0,
                        AssetLocationType = (reader["AssetLocationType"] as int?) ?? 0,
                        AssetLocationOrder = reader["AssetLocationOrder"] as string ?? string.Empty,

                    });
                }
            }
            return assets;
        }


        public IEnumerable<CaseStudy> GetCaseAssets()
        {
            var caseAssets = new List<CaseStudy>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetCaseStudies"))
            {
                while (reader.Read())
                {
                    caseAssets.Add(new CaseStudy { CaseId = (reader["CaseID"] as int?) ?? 0, CaseName = (reader["CaseName"] as string) ?? string.Empty, CaseOrder = (reader["CaseOrder"] as int?) ?? -1 });
                }
            }

            return caseAssets;
        }

        public void AssignAssetsToProgram(int programId, int testId, int type, int assetGroupId)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[2] = new SqlParameter("@Type", SqlDbType.Int, 4) { Value = type };
            sqlParameters[3] = new SqlParameter("@AssetGroupId", SqlDbType.Int, 4) { Value = assetGroupId };
            _dataContext.ExecuteStoredProcedure("USPAssignAssetsToProgram", sqlParameters);
        }

        public IEnumerable<Program> GetProgramsByProgramofStudyId(int programofStudyId)
        {
            var nurPrograms = new List<Program>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofStudyId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetProgramsByProgramofStudyId", sqlParameters))
            {
                while (reader.Read())
                {
                    nurPrograms.Add(new Program
                    {
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        ProgramName = (reader["ProgramName"] as String) ?? string.Empty,
                        Description = (reader["Description"] as String) ?? string.Empty,
                    });
                }
            }

            return nurPrograms;
        }

        public int GetInstitutionIdByFacilityIdOrClassCode(int facilityId, string classCode)
        {
            int institutionId = 0;

            if (classCode != null)
            {
                #region Sql Parameters

                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FacilityId", SqlDbType.Int, 4) { Value = facilityId };
                sqlParameters[1] = new SqlParameter("@ClassCode", SqlDbType.VarChar, 1000) { Value = classCode };

                #endregion

                institutionId = (_dataContext.ExecuteScalar("uspGetInstitutionIDByFacilityIdOrCohortDescription", sqlParameters) as int?) ?? 0;
            }

            return institutionId;
        }

        public IEnumerable<Student> GetStudentsByName(string userName)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 80) { Value = userName };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetStudentByUserName", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["UserId"] as int?) ?? 0,
                        UserName = (reader["UserName"] as String) ?? string.Empty,
                    });
                }
            }

            return students;
        }

        public string GetUniqueUsername(string firstName, string lastName)
        {
            string userName = string.Empty;
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 80) { Value = firstName };
            sqlParameters[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 80) { Value = lastName };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetUniqueUserName", sqlParameters))
            {
                while (reader.Read())
                {
                    userName = (reader["UserName"] as String) ?? string.Empty;
                }
            }

            return userName;
        }

        public List<LtiProvider> GetLtiProviders(int ltiProviderId)
        {
            var ltiProviders = new List<LtiProvider>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@LTIProviderId", SqlDbType.Int) { Value = ltiProviderId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetLTIProviders", sqlParameters))
            {
                while (reader.Read())
                {
                    ltiProviders.Add(new LtiProvider
                    {
                        Id = (reader["id"] as int?) ?? 0,
                        Name = (reader["name"] as String) ?? string.Empty,
                        Title = (reader["title"] as String) ?? string.Empty,
                        Url = (reader["url"] as String) ?? string.Empty,
                        Description = (reader["description"] as String) ?? string.Empty,
                        ConsumerKey = (reader["consumerKey"] as String) ?? string.Empty,
                        ConsumerSecret = (reader["consumerSecret"] as String) ?? string.Empty,
                        CustomParameters = (reader["customParameters"] as String) ?? string.Empty,
                        Active = (reader["active"] as bool?) ?? false
                    });
                }
            }

            return ltiProviders.ToList();
        }

        public LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName)
        {
            var ltiProvider = new LtiProvider();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@LTIProviderName", SqlDbType.VarChar) { Value = ltiProviderName };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetLtiProviderByName", sqlParameters))
            {
                while (reader.Read())
                {
                    ltiProvider.Id = (reader["id"] as int?) ?? 0;
                    ltiProvider.Name = ltiProviderName;
                    ltiProvider.Title = (reader["title"] as String) ?? string.Empty;
                    ltiProvider.Url = (reader["url"] as String) ?? string.Empty;
                    ltiProvider.Description = (reader["description"] as String) ?? string.Empty;
                    ltiProvider.ConsumerKey = (reader["consumerKey"] as String) ?? string.Empty;
                    ltiProvider.ConsumerSecret = (reader["consumerSecret"] as String) ?? string.Empty;
                    ltiProvider.Active = (bool) reader["active"];

                }
            }

            return ltiProvider;
        }

        public int SaveLtiProvider(LtiProvider ltiProvider)
        {
            int outputParameter = 9;
            #region SqlParameters
            var sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = ltiProvider.Id };
            sqlParameters[1] = new SqlParameter("@Name", SqlDbType.VarChar, 50) { Value = ltiProvider.Name };
            sqlParameters[2] = new SqlParameter("@Title", SqlDbType.VarChar, 100) { Value = ltiProvider.Title };
            sqlParameters[3] = new SqlParameter("@Url", SqlDbType.VarChar, 100) { Value = ltiProvider.Url };
            sqlParameters[4] = new SqlParameter("@Description", SqlDbType.VarChar, 100) { Value = ltiProvider.Description };
            sqlParameters[5] = new SqlParameter("@ConsumerKey", SqlDbType.VarChar, 100) { Value = ltiProvider.ConsumerKey };
            sqlParameters[6] = new SqlParameter("@ConsumerSecret", SqlDbType.VarChar, 100) { Value = ltiProvider.ConsumerSecret };
            sqlParameters[7] = new SqlParameter("@CustomParameters", SqlDbType.VarChar, 2000) { Value = ltiProvider.CustomParameters };
            sqlParameters[8] = new SqlParameter("@Active", SqlDbType.Bit) { Value = ltiProvider.Active };
            sqlParameters[outputParameter] = new SqlParameter("@newLtiProviderId", SqlDbType.Int, 4);

            sqlParameters[outputParameter].Direction = ParameterDirection.Output;
            #endregion
            _dataContext.ExecuteNonQuery("USPSaveLtiProvider", sqlParameters);
            int newLtiProviderId = (int)sqlParameters[outputParameter].Value;
            return newLtiProviderId;
        }

        public void ChangeLtiProviderStatus(int ltiProviderId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@LtiProviderId", SqlDbType.Int) { Value = ltiProviderId };
            #endregion
            _dataContext.ExecuteNonQuery("uspChangeLtiProviderStatus", sqlParameters);
        }

        public void CopyProgramTests(int originalProgramIdId, int newProgramId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@OriginalProgramId", SqlDbType.Int, 6) { Value = originalProgramIdId };
            sqlParameters[1] = new SqlParameter("@NewProgramId", SqlDbType.Int, 6) { Value = newProgramId };

            #endregion

            _dataContext.ExecuteStoredProcedure("uspCopyProgramTests", sqlParameters);
        }
        #endregion
    }
}
