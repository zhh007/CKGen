// -----------------------------------------------------------------------
// <copyright file="_EntitiesProjectTemplate.cs" company="Microsoft">
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
    public partial class EntitiesProjectTemplate
    {
        public string EntitiesProjID;
        public string EntitiesProjName;
        public List<string> FilePaths = null;

        public EntitiesProjectTemplate(Guid entitiesProjID, string entitiesProjName, List<string> filepaths)
        {
            this.EntitiesProjID = entitiesProjID.ToString().ToUpper();
            this.EntitiesProjName = entitiesProjName;
            this.FilePaths = filepaths;
        }
    }
}
