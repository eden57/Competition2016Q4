using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geone.Test.DAL;
using Geone.Test.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Geone.Test.BLL
{
    public class PolygonInersection
    {
        private static object _lock = new object();

        private List<RoadEntity> _index = new List<RoadEntity>();

        private IntersectionDAL _interDAL = new IntersectionDAL();


        private int _step = 0;

        /// <summary>
        /// 初始化重置
        /// </summary>
        private void InitReset()
        {
            _interDAL.ResetResult();

            _index = _interDAL.GetRoadSummry().ToList();
        }




        public void Start(Action<double, bool> callBack)
        {
            try
            {
                InitReset();


                Thread[] thread = new Thread[3];
                _step = 0;

                for (int i = 0, length = thread.Length; i < length; i++)
                {
                    thread[i] = new Thread(() =>
                    {
                        while (true)
                        {

                            try
                            {
                                RoadEntity entity = null;
                                lock (_lock)
                                {
                                    if (_index.Count > _step)
                                    {
                                        entity = _index[_step];
                                        _step++;
                                    }
                                }

                                if (entity == null)
                                {
                                    //if (_step >= _index.Count && callBack != null) callBack(1, true);
                                    Thread.CurrentThread.Abort();
                                    break;
                                }

                                //if (callBack != null) callBack(_step * 1.0 / _index.Count, false);

                                new IntersectionDAL().AnalysisIntersection(entity.IFQ, entity.DLBM);
                            }
                            catch { }

                        }

                    }) { IsBackground = true, Priority = ThreadPriority.AboveNormal, Name = "分析" + i };


                    thread[i].Start();
                }


                new Thread(() =>
                {
                    bool complete = true;
                    int count = 0;
                    while (true)
                    {
                        count = 0;
                        foreach (var item in thread)
                        {
                            if (item.IsAlive)
                            {
                                complete = false;
                                count++;
                            }
                        }
                        Thread.Sleep(1000);

                        if (callBack != null) callBack((_step - count) * 1.0 / _index.Count, false);
                        if (complete) break;
                    }

                    if (callBack != null) callBack(1, true);


                }) { Priority = ThreadPriority.Normal, IsBackground = true }.Start();

            }
            catch (Exception ex)
            {

                throw;
            }



        }





    }
}
