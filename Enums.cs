﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerX
{
    public enum OperationType
    {
        Append,
        Prepend,
        Replace,
        InsertAt,
        [Description("Delete files less than specific resolution")]
        DeleteFilesLessThanResolution,
        [Description("Organize files by Date Created")]
        OrganizeFilesDateCreated,
        [Description("Organize photos by Date Taken")]
        OrganizePhotos,
        [Description("Remove empty folders")]
        RemoveEmptyFolders
    }
}