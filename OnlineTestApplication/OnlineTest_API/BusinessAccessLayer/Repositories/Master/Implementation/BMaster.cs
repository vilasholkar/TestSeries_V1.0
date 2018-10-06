using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Master;

namespace BusinessAccessLayer
{
    public class BMaster : IBMaster
    {
        private readonly IDMaster _iDMaster;
        public BMaster(IDMaster iDMaster)
        {
            _iDMaster = iDMaster;
        }
        public Response<List<StreamViewModel>> GetStream()
        {
            try
            {
                var masterData = _iDMaster.GetStream();
                if (masterData != null)
                {
                    return new Response<List<StreamViewModel>>
                    {
                        IsSuccessful = true,
                        Object = masterData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<StreamViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Response<List<CourseViewModel>> GetCourseByStream(string StreamId)
        {
            try
            {
                var courseData = _iDMaster.GetCourseByStream(StreamId);
                if (courseData != null)
                {
                    return new Response<List<CourseViewModel>>
                    {
                        IsSuccessful = true,
                        Object = courseData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<CourseViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Response<List<BatchViewModel>> GetBatchByCourse(string CourseId)
        {
            try
            {
                var batchData = _iDMaster.GetBatchByCourse(CourseId);
                if (batchData != null)
                {
                    return new Response<List<BatchViewModel>>
                    {
                        IsSuccessful = true,
                        Object = batchData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<BatchViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Response<List<SessionViewModel>> GetSession()
        {
            try
            {
                var sessionData = _iDMaster.GetSession();
                if (sessionData != null)
                {
                    return new Response<List<SessionViewModel>>
                    {
                        IsSuccessful = true,
                        Object = sessionData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<SessionViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
