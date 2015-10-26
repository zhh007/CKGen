// -----------------------------------------------------------------------
// <copyright file="_slnTemplate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class slnTemplate
    {
        public string SolutionID;
        public string EntitiesProjName;
        public string EntitiesProjID;
        public string TestProjName;
        public string TestProjID;

        public slnTemplate(Guid solutionID, string entitiesProjName, Guid entitiesProjID, string testProjName, Guid testProjID)
        {
            this.SolutionID = solutionID.ToString().ToUpper();
            this.EntitiesProjName = entitiesProjName;
            this.EntitiesProjID = entitiesProjID.ToString().ToUpper();
            this.TestProjName = testProjName;
            this.TestProjID = testProjID.ToString().ToUpper();
        }
    }
}
