using System;
using FluentNHibernate.Mapping;
using SimplePersistence.Example.WebApi.Models.Logging;

namespace SimplePersistence.Example.WebApi.UoW.NH.Mapping.Logging
{
    [CLSCompliant(false)]
    public class LogMapping : ClassMap<Log>
    {
        public LogMapping()
        {
            Table("Logs");
            Schema("Logging");

            Id(e => e.Id, "Id").GeneratedBy.Identity();

            References(e => e.Level, "LevelId").Not.Nullable().Cascade.All();
            References(e => e.Application, "ApplicationId").Not.Nullable().Cascade.All();
            Map(e => e.Logger).Not.Nullable().Length(256);
            Map(e => e.Thread).Not.Nullable().Length(64);
            Map(e => e.Message).Not.Nullable();
            Map(e => e.Exception).Nullable();

            this.MapCreatedMeta();
        }
    }
}