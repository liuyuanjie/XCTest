using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity.Map
{
    public class TemplateMap
    {
        public static void Configure(EntityTypeBuilder<Template> entityTypeBuilder)
        {
            // Need to use the old table name to map the entity Name.
            entityTypeBuilder.ToTable("Template");
        }
    }

}
