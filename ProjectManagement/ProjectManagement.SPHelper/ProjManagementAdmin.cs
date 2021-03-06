﻿using ProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.SPHelper
{
    public class ProjManagementAdmin
    {
        private static readonly string PROJMGMT_CONN_STRING;

        #region Stored Procedure Constants
        private const string PROC_GETALLPROJECTS = "dbo.GetAllProjects";
        private const string PROC_ADDNEWPROJECT = "dbo.AddNewProject";
        private const string PROC_GETPROJECTBYID = "dbo.GetProjectById";
        private const string PROC_SOFTDELETEPROJECT = "dbo.SoftDeleteProject";
        private const string PROC_UPDATEPROJECT = "dbo.UpdateProject";



        #endregion

        #region SqlParameter Constants
        private const string PARAM_RETURN = "@return";

        private const string PARAM_CHANGEDBY = "@changed_by";
        private const string PARAM_PROJECT_CREATEDDATE = "@changed_by";
        private const string PARAM_PROJECT_CHANGEDDATE = "@changed_date";
        private const string PARAM_PROJECT_IS_ACTIVE = "@is_active";

        private const string PARAM_PROJECT_ID = "@project_id";
        private const string PARAM_PROJECT_NAME = "@project_name";
        private const string PARAM_PROJECT_CODE = "@project_code";
        private const string PARAM_PROJECT_LEAD_NAME = "@project_lead_name";
        private const string PARAM_PROJECT_LEAD_ID = "@project_lead_id";



        public static SqlDataReader GetAllProjects(out int retValue)
        {
            retValue = -1;
            SqlDataReader dr = null;
            SqlParameter[] parms = GetGetAllProjectsParams();
            dr = ExecuteReader(PROC_GETALLPROJECTS, parms, out retValue);

            return dr;
        }

        private static SqlParameter[] GetGetAllProjectsParams()
        {
            SqlParameter[] sqlParms = SQLHelper.GetCachedParameters(PROC_GETALLPROJECTS);
            if (sqlParms == null)
            {
                sqlParms = new SqlParameter[]
                            {
                                new SqlParameter(PARAM_RETURN, SqlDbType.Int)
                               
                            };

                sqlParms[0].Direction = ParameterDirection.ReturnValue;
                SQLHelper.CacheParameters(PROC_GETALLPROJECTS, sqlParms);
            }

            //Assigning values to parameter
            sqlParms[0].Value = -1;
            return sqlParms;
        }
        #endregion

        #region AddNewProject
        public static int AddNewProject(ProjectInfo project, out int retValue)
        {
            retValue = -1;
            SqlParameter[] parms = GetAddProjectParams(project);
            return ExecuteNonQuery(PROC_ADDNEWPROJECT, parms, out retValue);
        }

        private static SqlParameter[] GetAddProjectParams(ProjectInfo project)
        {
            SqlParameter[] sqlParms = new SqlParameter[100];
            sqlParms = SQLHelper.GetCachedParameters(PROC_ADDNEWPROJECT);
            if (sqlParms == null)
            {
                sqlParms = new SqlParameter[]
                            {
                                new SqlParameter(PARAM_RETURN, SqlDbType.Int),
                                new SqlParameter(PARAM_PROJECT_NAME, SqlDbType.NVarChar, 100),
                                new SqlParameter(PARAM_PROJECT_CODE, SqlDbType.NVarChar, 100),
                                new SqlParameter(PARAM_PROJECT_LEAD_ID, SqlDbType.Int),
                                new SqlParameter(PARAM_CHANGEDBY, SqlDbType.NVarChar, 100)

                            };

                sqlParms[0].Direction = ParameterDirection.ReturnValue;
                SQLHelper.CacheParameters(PROC_ADDNEWPROJECT, sqlParms);
            }

            //Assigning values to parameter
            sqlParms[0].Value = -1;
            sqlParms[1].Value = project.ProjectName;
            sqlParms[2].Value = project.ProjectCode;
            sqlParms[3].Value = project.ProjectLeadId;
            sqlParms[4].Value = "anjani";

            return sqlParms;
        }
        #endregion

        #region GetProjectById
        public static SqlDataReader GetProjectById(int projectId, out int retValue)
        {
            retValue = -1;
            SqlDataReader dr = null;
            SqlParameter[] parms = GetProjectByIdParams(projectId);
            dr = ExecuteReader(PROC_GETPROJECTBYID, parms, out retValue);

            return dr;
        }

        private static SqlParameter[] GetProjectByIdParams(int projectId)
        {
            SqlParameter[] sqlParms = SQLHelper.GetCachedParameters(PROC_GETPROJECTBYID);
            if (sqlParms == null)
            {
                sqlParms = new SqlParameter[]
                            {
                                new SqlParameter(PARAM_PROJECT_ID, SqlDbType.Int),
                                new SqlParameter(PARAM_RETURN, SqlDbType.Int)
                            };

                sqlParms[1].Direction = ParameterDirection.ReturnValue;
                SQLHelper.CacheParameters(PROC_GETPROJECTBYID, sqlParms);
            }

            //Assigning values to parameter
            sqlParms[0].Value = projectId;
            sqlParms[1].Value = -1;
            return sqlParms;
        }
        #endregion

        #region SoftDeleteProject

        public static int SoftDeleteProject(int projectId,bool isOpen, out int retValue)
        {
            retValue = -1;
            SqlParameter[] parms = GetSoftDeleteProjectParams(projectId,isOpen);
            return ExecuteNonQuery(PROC_SOFTDELETEPROJECT, parms, out retValue);
        }

        private static SqlParameter[] GetSoftDeleteProjectParams(int projectId,bool isOpen)
        {
            SqlParameter[] sqlParms = new SqlParameter[100];
            sqlParms = SQLHelper.GetCachedParameters(PROC_SOFTDELETEPROJECT);
            if (sqlParms == null)
            {
                sqlParms = new SqlParameter[]
                            {
                                new SqlParameter(PARAM_PROJECT_ID, SqlDbType.Int),
                                new SqlParameter(PARAM_PROJECT_IS_ACTIVE,SqlDbType.Bit),
                                new SqlParameter(PARAM_RETURN, SqlDbType.Int)
                            };

                sqlParms[2].Direction = ParameterDirection.ReturnValue;
                SQLHelper.CacheParameters(PROC_SOFTDELETEPROJECT, sqlParms);
            }

            //Assigning values to parameter

            sqlParms[0].Value = projectId;
            sqlParms[1].Value = Convert.ToByte(isOpen);
            sqlParms[2].Value = -1;
            return sqlParms;
        }
        #endregion

        #region UpdateProject

        public static int UpdateProject(ProjectInfo project, out int retValue)
        {
            retValue = -1;
            SqlParameter[] parms = GetUpdateProjectParams(project);
            return ExecuteNonQuery(PROC_UPDATEPROJECT, parms, out retValue);
        }

        private static SqlParameter[] GetUpdateProjectParams(ProjectInfo project)
        {
            SqlParameter[] sqlParms = new SqlParameter[100];
            sqlParms = SQLHelper.GetCachedParameters(PROC_UPDATEPROJECT);
            if (sqlParms == null)
            {
                sqlParms = new SqlParameter[]
                            {
                                new SqlParameter(PARAM_PROJECT_ID,SqlDbType.Int),
                                new SqlParameter(PARAM_PROJECT_NAME, SqlDbType.NVarChar, 100),
                                new SqlParameter(PARAM_PROJECT_CODE, SqlDbType.NVarChar, 100),
                                new SqlParameter(PARAM_PROJECT_LEAD_ID, SqlDbType.Int),
                                new SqlParameter(PARAM_CHANGEDBY, SqlDbType.NVarChar, 100),
                                new SqlParameter(PARAM_RETURN, SqlDbType.Int)

                            };

                sqlParms[5].Direction = ParameterDirection.ReturnValue;
                SQLHelper.CacheParameters(PROC_UPDATEPROJECT, sqlParms);
            }

            //Assigning values to parameter
            sqlParms[0].Value = project.ProjectId;
            sqlParms[1].Value = project.ProjectName;
            sqlParms[2].Value = project.ProjectCode;
            sqlParms[3].Value = project.ProjectLeadId;
            sqlParms[4].Value = "anjani";
            sqlParms[5].Value = -1;
            return sqlParms;
        }
        #endregion


        static ProjManagementAdmin()
        {
            PROJMGMT_CONN_STRING = ConfigurationManager.ConnectionStrings["ProjectManagementConnectionString"].ConnectionString;
        }

        #region SQL Methods

        private static void ThrowIfNullParams(string spName, SqlParameter[] parms)
        {
            if (parms == null)
                throw new ArgumentException("Couldn't build the parameters for procedure:" + spName);
        }
        private static SqlDataReader ExecuteReader(string spName, SqlParameter[] parms, out int retValue)
        {
            ThrowIfNullParams(spName, parms);
            retValue = -1;
            return SQLHelper.ExecuteReader(PROJMGMT_CONN_STRING, CommandType.StoredProcedure, spName, out retValue, parms);
        }

        private static int ExecuteNonQuery(string spName, SqlParameter[] parms, out int retValue)
        {
            ThrowIfNullParams(spName, parms);
            retValue = -1;
            return SQLHelper.ExecuteNonQuery(PROJMGMT_CONN_STRING, CommandType.StoredProcedure, spName, out retValue, parms);
        }
        #endregion

    }
}
