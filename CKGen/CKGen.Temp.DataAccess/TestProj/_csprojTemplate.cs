// -----------------------------------------------------------------------
// <copyright file="_.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess.TestProj
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class csprojTemplate
    {
        private string TestProjectID;
        private string TestProjectName;
        private string EntitiesProjectID;
        private string EntitiesProjName;

        public csprojTemplate(string entitiesProjName, Guid entitiesProjID, string testProjName, Guid testProjID)
        {
            //if (string.IsNullOrEmpty(projName))
            //    throw new Exception("参数connName，命名空间不能为空。");

            this.TestProjectName = testProjName;

            if (string.IsNullOrEmpty(entitiesProjName))
                throw new Exception("参数connString，命名空间不能为空。");

            this.EntitiesProjName = entitiesProjName;

            this.TestProjectID = testProjID.ToString().ToUpper();
            this.EntitiesProjectID = entitiesProjID.ToString().ToUpper();
            //this.ProjectName = solutionName + ".Test";
        }
    }
}
