﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InFrameDAL.Models
{
    public partial class InFrameContext : DbContext
    {
        public InFrameContext()
        {
        }

        public InFrameContext(DbContextOptions<InFrameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Demand> Demand { get; set; }
        public virtual DbSet<DemandDynProp> DemandDynProp { get; set; }
        public virtual DbSet<DemandType> DemandType { get; set; }
        public virtual DbSet<DemandTypeDemandDynProp> DemandTypeDemandDynProp { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormField> FormField { get; set; }
        public virtual DbSet<FormGroup> FormGroup { get; set; }
        public virtual DbSet<Transition> Transition { get; set; }
        public virtual DbSet<TransitionStartState> TransitionStartState { get; set; }
        public virtual DbSet<ValueDemandDynProp> ValueDemandDynProp { get; set; }
        public virtual DbSet<ValueDemandDynPropHisto> ValueDemandDynPropHisto { get; set; }
        public virtual DbSet<WorkFlow> WorkFlow { get; set; }
        public virtual DbSet<WorkFlowTransition> WorkFlowTransition { get; set; }
        public virtual DbSet<WorkflowState> WorkflowState { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=InFrame;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demand>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DemandTypeid).HasColumnName("demandTypeid");

                entity.Property(e => e.EtatId).HasColumnName("etatId");

                entity.Property(e => e.WorkFlowId).HasColumnName("workFlowId");

                entity.HasOne(d => d.DemandType)
                    .WithMany(p => p.Demand)
                    .HasForeignKey(d => d.DemandTypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Demand_demandTypeId");

                entity.HasOne(d => d.WorkFlow)
                    .WithMany(p => p.Demand)
                    .HasForeignKey(d => d.WorkFlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Demand_workflowId");
            });

            modelBuilder.Entity<DemandDynProp>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DemandDynPropName)
                    .IsRequired()
                    .HasColumnName("demandDynPropName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DemandType)
                    .IsRequired()
                    .HasColumnName("demandType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");
            });

            modelBuilder.Entity<DemandType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DemandTypeDescription)
                    .HasColumnName("demandTypeDescription")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DemandTypeInternalName)
                    .IsRequired()
                    .HasColumnName("demandTypeInternalName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DemandTypeName)
                    .IsRequired()
                    .HasColumnName("demandTypeName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DemandTypeShortName)
                    .IsRequired()
                    .HasColumnName("demandTypeShortName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowId).HasColumnName("workflowId");

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.DemandType)
                    .HasForeignKey(d => d.WorkflowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DemandType_workflowId");
            });

            modelBuilder.Entity<DemandTypeDemandDynProp>(entity =>
            {
                entity.ToTable("DemandType_DemandDynProp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DemandDynPropId).HasColumnName("demandDynPropId");

                entity.Property(e => e.DemandTypeId).HasColumnName("demandTypeId");

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");

                entity.HasOne(d => d.DemandDynProp)
                    .WithMany(p => p.DemandTypeDemandDynProp)
                    .HasForeignKey(d => d.DemandDynPropId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DemandType_DemandDynProp_demandDynPropId");

                entity.HasOne(d => d.DemandType)
                    .WithMany(p => p.DemandTypeDemandDynProp)
                    .HasForeignKey(d => d.DemandTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DemandType_DemandDynProp_demandTypeId");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Behavior).HasColumnName("behavior");

                entity.Property(e => e.ColumnNumber).HasColumnName("columnNumber");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormField>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Behavior).HasColumnName("behavior");

                entity.Property(e => e.DefaultValue)
                    .IsRequired()
                    .HasColumnName("defaultValue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DemandTypeId).HasColumnName("demandTypeId");

                entity.Property(e => e.FieldLabel)
                    .IsRequired()
                    .HasColumnName("fieldLabel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasColumnName("fieldName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldOrder).HasColumnName("fieldOrder");

                entity.Property(e => e.FieldParameters)
                    .IsRequired()
                    .HasColumnName("fieldParameters")
                    .IsUnicode(false);

                entity.Property(e => e.FieldType)
                    .IsRequired()
                    .HasColumnName("fieldType")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDynamic).HasColumnName("isDynamic");

                entity.Property(e => e.Tooltip)
                    .IsRequired()
                    .HasColumnName("tooltip")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");
            });

            modelBuilder.Entity<FormGroup>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Behavior).HasColumnName("behavior");

                entity.Property(e => e.ColumnIndex).HasColumnName("columnIndex");

                entity.Property(e => e.FormId).HasColumnName("formId");

                entity.Property(e => e.GroupOrder).HasColumnName("groupOrder");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Transition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actions)
                    .HasColumnName("actions")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.AffichagePriority).HasColumnName("affichagePriority");

                entity.Property(e => e.Behavior).HasColumnName("behavior");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.EndStateId).HasColumnName("endStateId");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.InternalName)
                    .IsRequired()
                    .HasColumnName("internalName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransitionName)
                    .IsRequired()
                    .HasColumnName("transitionName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransitionShortName)
                    .IsRequired()
                    .HasColumnName("transitionShortName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");

                entity.HasOne(d => d.EndState)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.EndStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Transition_endStateId");
            });

            modelBuilder.Entity<TransitionStartState>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StartStateId).HasColumnName("startStateId");

                entity.Property(e => e.TransitionId).HasColumnName("transitionId");

                entity.HasOne(d => d.StartState)
                    .WithMany(p => p.TransitionStartState)
                    .HasForeignKey(d => d.StartStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TransitionStartState_startStateId");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.TransitionStartState)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TransitionStartState_transitionId");
            });

            modelBuilder.Entity<ValueDemandDynProp>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangeDate)
                    .HasColumnName("changeDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateValue)
                    .HasColumnName("dateValue")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecimalValue)
                    .HasColumnName("decimalValue")
                    .HasColumnType("decimal(30, 10)");

                entity.Property(e => e.DemandDynPropId).HasColumnName("demandDynPropId");

                entity.Property(e => e.DemandId).HasColumnName("demandId");

                entity.Property(e => e.IntValue).HasColumnName("intValue");

                entity.Property(e => e.RealValue).HasColumnName("realValue");

                entity.Property(e => e.StringValue)
                    .HasColumnName("stringValue")
                    .IsUnicode(false);

                entity.HasOne(d => d.DemandDynProp)
                    .WithMany(p => p.ValueDemandDynProp)
                    .HasForeignKey(d => d.DemandDynPropId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ValueDemandDynProp_demandDynPropId");

                entity.HasOne(d => d.Demand)
                    .WithMany(p => p.ValueDemandDynProp)
                    .HasForeignKey(d => d.DemandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ValueDemandDynProp_demandId");
            });

            modelBuilder.Entity<ValueDemandDynPropHisto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangeDate)
                    .HasColumnName("changeDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateValue)
                    .HasColumnName("dateValue")
                    .HasColumnType("datetime");

                entity.Property(e => e.DecimalValue)
                    .HasColumnName("decimalValue")
                    .HasColumnType("decimal(30, 10)");

                entity.Property(e => e.DemandDynPropId).HasColumnName("demandDynPropId");

                entity.Property(e => e.DemandId).HasColumnName("demandId");

                entity.Property(e => e.IntValue).HasColumnName("intValue");

                entity.Property(e => e.RealValue).HasColumnName("realValue");

                entity.Property(e => e.StringValue)
                    .HasColumnName("stringValue")
                    .IsUnicode(false);

                entity.HasOne(d => d.DemandDynProp)
                    .WithMany(p => p.ValueDemandDynPropHisto)
                    .HasForeignKey(d => d.DemandDynPropId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ValueDemandDynPropHisto_demandDynPropId");

                entity.HasOne(d => d.Demand)
                    .WithMany(p => p.ValueDemandDynPropHisto)
                    .HasForeignKey(d => d.DemandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ValueDemandDynPropHisto_demandId");
            });

            modelBuilder.Entity<WorkFlow>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StartStateId).HasColumnName("startStateId");

                entity.Property(e => e.WorkflowDescription)
                    .HasColumnName("workflowDescription")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowName)
                    .IsRequired()
                    .HasColumnName("workflowName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowShortName)
                    .IsRequired()
                    .HasColumnName("workflowShortName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.StartState)
                    .WithMany(p => p.WorkFlow)
                    .HasForeignKey(d => d.StartStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_WorkFlow_startStateId");
            });

            modelBuilder.Entity<WorkFlowTransition>(entity =>
            {
                entity.ToTable("WorkFlow_Transition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TransitionId).HasColumnName("transitionId");

                entity.Property(e => e.WorkflowId).HasColumnName("workflowId");

                entity.Property(e => e.WorkflowState).HasColumnName("workflowState");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.WorkFlowTransition)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_WorkFlow_Transition_transitionId");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.WorkFlowTransition)
                    .HasForeignKey(d => d.WorkflowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_WorkFlow_Transition_workflowId");
            });

            modelBuilder.Entity<WorkflowState>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.StateDescription)
                    .HasColumnName("stateDescription")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnName("stateName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StateShortName)
                    .IsRequired()
                    .HasColumnName("stateShortName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorkflowState1).HasColumnName("workflowState");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}