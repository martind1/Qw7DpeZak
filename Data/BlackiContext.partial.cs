﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QwTest7.Models.Blacki;

namespace QwTest7.Data;

public partial class BlackiContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        #region Bulk configuration via model class for all table names
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            // All table names = class names (~ EF 6.x), 
            // except the classes that have a [Table] annotation or derived classes (where ToTable() is not allowed in EF Core >= 3.0)
            var annotation = entity.ClrType?.GetCustomAttribute<TableAttribute>();
            if (annotation == null && entity.BaseType == null)
            {
                //entity.Relational().TableName = entity.DisplayName();
            }
        }
        #endregion
    }
}
