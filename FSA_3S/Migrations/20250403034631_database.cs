using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clause",
                columns: table => new
                {
                    clauseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clauseNumber = table.Column<int>(type: "int", nullable: false),
                    clauseContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clauseType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clause", x => x.clauseId);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    customertype = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    reportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfContracts = table.Column<int>(type: "int", nullable: true),
                    NumberOfRealEstates = table.Column<int>(type: "int", nullable: true),
                    NumberOfAppointments = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.reportId);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cccd = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    timeofwork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    typeofstaff = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    appointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.appointmentId);
                    table.ForeignKey(
                        name: "FK_appointment_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_user_createdBy",
                        column: x => x.createdBy,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_user_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.IdNotification);
                    table.ForeignKey(
                        name: "FK_Notifications_user_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_user_SenderId",
                        column: x => x.SenderId,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "realestate",
                columns: table => new
                {
                    realEstateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    approval = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sellerid = table.Column<int>(type: "int", nullable: false),
                    coordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saledate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    image_path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    bedrooms = table.Column<int>(type: "int", nullable: true),
                    bathrooms = table.Column<int>(type: "int", nullable: true),
                    is_furnished = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_realestate", x => x.realEstateId);
                    table.ForeignKey(
                        name: "FK_realestate_customer_sellerid",
                        column: x => x.sellerid,
                        principalTable: "customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_realestate_user_createdBy",
                        column: x => x.createdBy,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_realestate_user_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "work",
                columns: table => new
                {
                    work_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Monday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MondayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuesdayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wednesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WednesdayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thursday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThursdayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Friday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FridayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saturday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaturdayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sunday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SundayTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEntityUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work", x => x.work_id);
                    table.ForeignKey(
                        name: "FK_work_user_UserEntityUserId",
                        column: x => x.UserEntityUserId,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "mappinguserappointment",
                columns: table => new
                {
                    mappingUserAppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    approval = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEntityUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappinguserappointment", x => x.mappingUserAppointmentId);
                    table.ForeignKey(
                        name: "FK_mappinguserappointment_appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "appointment",
                        principalColumn: "appointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mappinguserappointment_user_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mappinguserappointment_user_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mappinguserappointment_user_UserEntityUserId",
                        column: x => x.UserEntityUserId,
                        principalTable: "user",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_mappinguserappointment_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mappingusernotification",
                columns: table => new
                {
                    mappingUserNotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    UserEntityUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappingusernotification", x => x.mappingUserNotificationId);
                    table.ForeignKey(
                        name: "FK_mappingusernotification_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "IdNotification",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mappingusernotification_user_UserEntityUserId",
                        column: x => x.UserEntityUserId,
                        principalTable: "user",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_mappingusernotification_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    contractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    realEstateId = table.Column<int>(type: "int", nullable: true),
                    contracttype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contractstatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    statuspayment = table.Column<int>(type: "int", nullable: false),
                    startdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    enddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.contractId);
                    table.ForeignKey(
                        name: "FK_contract_realestate_realEstateId",
                        column: x => x.realEstateId,
                        principalTable: "realestate",
                        principalColumn: "realEstateId");
                    table.ForeignKey(
                        name: "FK_contract_user_createdBy",
                        column: x => x.createdBy,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contract_user_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "audit",
                columns: table => new
                {
                    auditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contractId = table.Column<int>(type: "int", nullable: true),
                    realEstateId = table.Column<int>(type: "int", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit", x => x.auditId);
                    table.ForeignKey(
                        name: "FK_audit_contract_contractId",
                        column: x => x.contractId,
                        principalTable: "contract",
                        principalColumn: "contractId");
                    table.ForeignKey(
                        name: "FK_audit_realestate_realEstateId",
                        column: x => x.realEstateId,
                        principalTable: "realestate",
                        principalColumn: "realEstateId");
                    table.ForeignKey(
                        name: "FK_audit_user_createdBy",
                        column: x => x.createdBy,
                        principalTable: "user",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_audit_user_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "mappingcontractclause",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ClauseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappingcontractclause", x => new { x.ContractId, x.ClauseId });
                    table.ForeignKey(
                        name: "FK_mappingcontractclause_clause_ClauseId",
                        column: x => x.ClauseId,
                        principalTable: "clause",
                        principalColumn: "clauseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mappingcontractclause_contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contract",
                        principalColumn: "contractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mappingcontractcustomer",
                columns: table => new
                {
                    mappingContractCustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contractId = table.Column<int>(type: "int", nullable: false),
                    buyerId = table.Column<int>(type: "int", nullable: true),
                    sellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappingcontractcustomer", x => x.mappingContractCustomerId);
                    table.ForeignKey(
                        name: "FK_mappingcontractcustomer_contract_contractId",
                        column: x => x.contractId,
                        principalTable: "contract",
                        principalColumn: "contractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mappingcontractcustomer_customer_buyerId",
                        column: x => x.buyerId,
                        principalTable: "customer",
                        principalColumn: "customerId");
                    table.ForeignKey(
                        name: "FK_mappingcontractcustomer_customer_sellerId",
                        column: x => x.sellerId,
                        principalTable: "customer",
                        principalColumn: "customerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_createdBy",
                table: "appointment",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_CustomerId",
                table: "appointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_updatedBy",
                table: "appointment",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_audit_contractId",
                table: "audit",
                column: "contractId");

            migrationBuilder.CreateIndex(
                name: "IX_audit_createdBy",
                table: "audit",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_audit_realEstateId",
                table: "audit",
                column: "realEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_audit_updatedBy",
                table: "audit",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_contract_createdBy",
                table: "contract",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_contract_realEstateId",
                table: "contract",
                column: "realEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_contract_updatedBy",
                table: "contract",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_mappingcontractclause_ClauseId",
                table: "mappingcontractclause",
                column: "ClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingcontractcustomer_buyerId",
                table: "mappingcontractcustomer",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingcontractcustomer_contractId",
                table: "mappingcontractcustomer",
                column: "contractId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingcontractcustomer_sellerId",
                table: "mappingcontractcustomer",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_mappinguserappointment_AppointmentId",
                table: "mappinguserappointment",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mappinguserappointment_CreatedBy",
                table: "mappinguserappointment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_mappinguserappointment_UpdatedBy",
                table: "mappinguserappointment",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_mappinguserappointment_UserEntityUserId",
                table: "mappinguserappointment",
                column: "UserEntityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_mappinguserappointment_UserId",
                table: "mappinguserappointment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingusernotification_NotificationId",
                table: "mappingusernotification",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingusernotification_UserEntityUserId",
                table: "mappingusernotification",
                column: "UserEntityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_mappingusernotification_UserId",
                table: "mappingusernotification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_realestate_createdBy",
                table: "realestate",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_realestate_sellerid",
                table: "realestate",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_realestate_updatedBy",
                table: "realestate",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_work_UserEntityUserId",
                table: "work",
                column: "UserEntityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit");

            migrationBuilder.DropTable(
                name: "mappingcontractclause");

            migrationBuilder.DropTable(
                name: "mappingcontractcustomer");

            migrationBuilder.DropTable(
                name: "mappinguserappointment");

            migrationBuilder.DropTable(
                name: "mappingusernotification");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "work");

            migrationBuilder.DropTable(
                name: "clause");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "realestate");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
