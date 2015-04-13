﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
    public class JobBLL
    {
        public static List<IJob> GetJobs(SearchCriteria searchCriteria, int ID, string JobType) 
        {
            return JobDAL.GetJobs(searchCriteria, ID, JobType);
        }

        public static int AddEditJob(IJob Jobs, int CompanyId, ref int JobId)
        {
            return JobDAL.AddEditJob(Jobs, CompanyId, ref JobId);
        }

        public static int DeleteJob(int JobID, int UserID)
        {
            return JobDAL.DeleteJob(JobID, UserID);
        }

        public static DataSet GetDashBoard(int JobId)
        {
            return JobDAL.GetDashBoard(JobId);
        }

        public static void UpdateJobStatus(int JobId, string Type, int UserId)
        {
            JobDAL.UpdateJobStatus(JobId, Type, UserId);
        }

        public static void SendConfMail(int JobId, string Type, int UserId, decimal GP)
        {
            JobDAL.SendConfMail(JobId, Type, UserId, GP);
        }

        public static void SaveEstimateFile(int EstimateId, string FileName, string OriginalFileName)
        {
            JobDAL.SaveEstimateFile(EstimateId, FileName, OriginalFileName);
        }

        public static void DeleteDashBoardData(int Id, string Type)
        {
            JobDAL.DeleteDashBoardData(Id, Type);
        }

        public static DataTable GetJobNoFromJobID(int JobID)
        {
            return JobDAL.GetJobNoFromJobID(JobID);
        }

        public static DataTable GetContainerCountFromJobID(int JobID)
        {
            return JobDAL.GetContainerCountFromJobID(JobID);
        }

        public void DeactivateAllContainersAgainstJobId(int JobId)
        {
            JobDAL.DeactivateAllContainersAgainstJobId(JobId);

        }
        public int AddEditJobContainer(IBookingContainer Containers)
        {
            return JobDAL.AddEditJobContainer(Containers);
        }

        public List<IBookingContainer> GetJobContainers(int JobID)
        {
            return JobDAL.GetJobContainers(JobID);
        }

    }
}
