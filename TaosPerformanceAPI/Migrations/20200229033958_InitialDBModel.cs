using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaosPerformanceAPI.Migrations
{
    public partial class InitialDBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    companyId = table.Column<int>(nullable: false),
                    catalogName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogs", x => new { x.id, x.companyId });
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    companyName = table.Column<string>(maxLength: 210, nullable: false),
                    legalAddress = table.Column<string>(maxLength: 250, nullable: true),
                    taxId = table.Column<string>(maxLength: 15, nullable: true),
                    laborCode = table.Column<string>(maxLength: 30, nullable: true),
                    userPrefix = table.Column<string>(maxLength: 3, nullable: true),
                    startPrefix = table.Column<int>(nullable: false),
                    endPrefix = table.Column<int>(nullable: false),
                    purchasedLicenses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee_goals_feedback",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    goalId = table.Column<int>(nullable: false),
                    wroteBy = table.Column<string>(maxLength: 10, nullable: false),
                    recordDate = table.Column<DateTime>(nullable: false),
                    feedback = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_goals_feedback", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employeeLinks",
                columns: table => new
                {
                    companyId = table.Column<int>(nullable: false),
                    leaderId = table.Column<string>(maxLength: 10, nullable: false),
                    employeeId = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeLinks", x => new { x.companyId, x.leaderId, x.employeeId });
                });

            migrationBuilder.CreateTable(
                name: "evaluation_answers",
                columns: table => new
                {
                    evaluationId = table.Column<int>(nullable: false),
                    templateId = table.Column<int>(nullable: false),
                    itemKey = table.Column<int>(nullable: false),
                    capabilityId = table.Column<int>(nullable: false),
                    skillId = table.Column<int>(nullable: false),
                    answerValue = table.Column<string>(maxLength: 50, nullable: true),
                    feedback = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation_answers", x => new { x.evaluationId, x.itemKey });
                });

            migrationBuilder.CreateTable(
                name: "evaluation_period",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    periodId = table.Column<int>(nullable: false),
                    templateId = table.Column<int>(nullable: false),
                    templateName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation_periods", x => new { x.id, x.periodId, x.templateId });
                });

            migrationBuilder.CreateTable(
                name: "evaluations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    companyId = table.Column<int>(nullable: false),
                    periodId = table.Column<int>(nullable: false),
                    employeeId = table.Column<string>(maxLength: 10, nullable: false),
                    leaderId = table.Column<string>(maxLength: 10, nullable: false),
                    evaluationDate = table.Column<DateTime>(nullable: false),
                    leaderComments = table.Column<string>(type: "text", nullable: true),
                    employeeComments = table.Column<string>(type: "text", nullable: true),
                    goals_score = table.Column<double>(nullable: false),
                    skills_score = table.Column<double>(nullable: false),
                    final_score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluation", x => new { x.id, x.companyId });
                });

            migrationBuilder.CreateTable(
                name: "goal_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    companyId = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 80, nullable: false),
                    description = table.Column<string>(maxLength: 150, nullable: true),
                    enabled = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_type", x => new { x.id, x.companyId });
                });

            migrationBuilder.CreateTable(
                name: "period_archive",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    companyId = table.Column<int>(nullable: false),
                    periodId = table.Column<int>(nullable: false),
                    capability_title = table.Column<string>(maxLength: 50, nullable: true),
                    expectedCapabilityScore = table.Column<double>(nullable: false),
                    skill_title = table.Column<string>(maxLength: 100, nullable: true),
                    expectedSkillScore = table.Column<double>(nullable: false),
                    paramKey = table.Column<int>(nullable: false),
                    paramDescription = table.Column<string>(maxLength: 250, nullable: true),
                    paramOptions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period_archive", x => new { x.id, x.companyId });
                });

            migrationBuilder.CreateTable(
                name: "period_capabilities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    periodId = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    shortCode = table.Column<string>(maxLength: 6, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    expectedScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period_capabilities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    base_template = table.Column<int>(nullable: false),
                    companyId = table.Column<int>(nullable: false, defaultValue: 0),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    enabled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "catalogs_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    idCatalog = table.Column<int>(nullable: false),
                    companyId = table.Column<int>(nullable: false),
                    data = table.Column<string>(maxLength: 210, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogs_data", x => new { x.id, x.idCatalog, x.companyId });
                    table.ForeignKey(
                        name: "FK_catalogs_data_catalogs_idCatalog_companyId",
                        columns: x => new { x.idCatalog, x.companyId },
                        principalTable: "catalogs",
                        principalColumns: new[] { "id", "companyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 10, nullable: false),
                    companyId = table.Column<int>(nullable: false),
                    employeeName = table.Column<string>(maxLength: 60, nullable: false),
                    fatherLastName = table.Column<string>(maxLength: 80, nullable: false),
                    motherLastName = table.Column<string>(maxLength: 80, nullable: true),
                    birthDate = table.Column<DateTime>(nullable: true),
                    socialSecurityNumber = table.Column<string>(maxLength: 15, nullable: true),
                    taxId = table.Column<string>(maxLength: 15, nullable: true),
                    curp = table.Column<string>(maxLength: 20, nullable: true),
                    payrollId = table.Column<string>(maxLength: 50, nullable: true),
                    leaderId = table.Column<string>(maxLength: 10, nullable: true),
                    departmentId = table.Column<int>(nullable: false),
                    jobTypeId = table.Column<int>(nullable: false),
                    employmentStartDate = table.Column<DateTime>(nullable: false),
                    employedSinceDate = table.Column<DateTime>(nullable: false),
                    contractType = table.Column<int>(nullable: false),
                    contractStartDate = table.Column<DateTime>(nullable: true),
                    contractEndDate = table.Column<DateTime>(nullable: true),
                    email = table.Column<string>(maxLength: 150, nullable: true),
                    enabled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => new { x.id, x.companyId });
                    table.UniqueConstraint("AK_employees_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "period_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    companyId = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 150, nullable: false),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period_details", x => new { x.id, x.companyId });
                    table.ForeignKey(
                        name: "FK_period_details_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 10, nullable: false),
                    companyId = table.Column<int>(nullable: false),
                    userKey = table.Column<string>(maxLength: 200, nullable: false),
                    userType = table.Column<int>(nullable: false),
                    loginAllowed = table.Column<int>(nullable: false),
                    lastLogin = table.Column<DateTime>(nullable: true),
                    displayName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "period_skills",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    capabilityId = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    expectedScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period_skills", x => new { x.id, x.capabilityId });
                    table.UniqueConstraint("AK_period_skills_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_period_skills_period_capabilities_capabilityId",
                        column: x => x.capabilityId,
                        principalTable: "period_capabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "capabilities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    templateId = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    shortCode = table.Column<string>(maxLength: 6, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    expectedScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capabilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_capabilities_templates_templateId",
                        column: x => x.templateId,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_goals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    employeeId = table.Column<string>(maxLength: 10, nullable: false),
                    periodId = table.Column<int>(nullable: false),
                    goalType = table.Column<int>(nullable: false),
                    goal = table.Column<string>(maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    dueDate = table.Column<DateTime>(nullable: false),
                    weight = table.Column<double>(nullable: false),
                    progressMade = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => new { x.id, x.periodId, x.employeeId });
                    table.ForeignKey(
                        name: "FK_employee_goals_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "period_parameters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    skillId = table.Column<int>(nullable: false),
                    itemKey = table.Column<int>(nullable: false),
                    itemPosition = table.Column<int>(nullable: false),
                    parameterType = table.Column<int>(maxLength: 20, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    enabled = table.Column<short>(nullable: false),
                    options = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period_parameters", x => new { x.id, x.skillId });
                    table.ForeignKey(
                        name: "FK_period_parameters_period_skills_skillId",
                        column: x => x.skillId,
                        principalTable: "period_skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    capabilityId = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    expectedScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => new { x.id, x.capabilityId });
                    table.UniqueConstraint("AK_skills_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_skills_capabilities_capabilityId",
                        column: x => x.capabilityId,
                        principalTable: "capabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parameters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    skillId = table.Column<int>(nullable: false),
                    itemKey = table.Column<int>(nullable: false),
                    itemPosition = table.Column<int>(nullable: false),
                    parameterType = table.Column<int>(maxLength: 20, nullable: false),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    enabled = table.Column<short>(nullable: false),
                    options = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parameters", x => new { x.id, x.skillId });
                    table.ForeignKey(
                        name: "FK_parameters_skills_skillId",
                        column: x => x.skillId,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_capabilities_templateId",
                table: "capabilities",
                column: "templateId");

            migrationBuilder.CreateIndex(
                name: "IX_catalogs_data_idCatalog_companyId",
                table: "catalogs_data",
                columns: new[] { "idCatalog", "companyId" });

            migrationBuilder.CreateIndex(
                name: "IX_employee_goals_employeeId",
                table: "employee_goals",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_companyId",
                table: "employees",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_parameters_skillId",
                table: "parameters",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_period_details_companyId",
                table: "period_details",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_period_parameters_skillId",
                table: "period_parameters",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_period_skills_capabilityId",
                table: "period_skills",
                column: "capabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_capabilityId",
                table: "skills",
                column: "capabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_users_companyId",
                table: "users",
                column: "companyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalogs_data");

            migrationBuilder.DropTable(
                name: "employee_goals");

            migrationBuilder.DropTable(
                name: "employee_goals_feedback");

            migrationBuilder.DropTable(
                name: "employeeLinks");

            migrationBuilder.DropTable(
                name: "evaluation_answers");

            migrationBuilder.DropTable(
                name: "evaluation_period");

            migrationBuilder.DropTable(
                name: "evaluations");

            migrationBuilder.DropTable(
                name: "goal_type");

            migrationBuilder.DropTable(
                name: "parameters");

            migrationBuilder.DropTable(
                name: "period_archive");

            migrationBuilder.DropTable(
                name: "period_details");

            migrationBuilder.DropTable(
                name: "period_parameters");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "catalogs");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "period_skills");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "capabilities");

            migrationBuilder.DropTable(
                name: "period_capabilities");

            migrationBuilder.DropTable(
                name: "templates");
        }
    }
}
