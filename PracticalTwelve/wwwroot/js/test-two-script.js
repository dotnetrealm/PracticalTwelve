$(document).ready(() => {

    //display toast message
    const makeToast = (message) => {
        var url = 'Toast/Index?message=' + encodeURIComponent(message);
        $.get(url, (res) => {
            $("#ToastDisplay").html(res);
            $('.toast').toast('show');
        })
    }

    //return toast element
    const ToastElement = (message) => {
        return `<div class="toast bg-info text-white align-items-center w-100" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body h6">
                    `+ message + `      
                </div>
                <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>`
    }

    //reload table data from DB
    const reloadTable = async () => {
        $("#EmployeeSalaryData").load("TestTwo/LoadUsers");
        $("#DisplaySection").html("");
        makeToast("User loaded successfully!");
    }

    //onPageLoad events
    reloadTable();

    //fetch all users
    $("#EmployeeSalaryTableActions #GetAllEmployees").on("click", () => {
        reloadTable();
    });

    //Insert mutliple records
    $("#EmployeeSalaryTableActions #InsertRecords").on("click", () => {
        let url = "TestTwo/InsertMultipleRecord";
        $.ajax({
            url: url,
            success: (res) => {
                makeToast(res.count + " " + "record inserted.");
                reloadTable();
            }
        });
    });

    //get total employee salary
    $("#GetTotalEmployeesSalary").on("click", (e) => {
        let url = "TestTwo/GetTotalEmployeesSalary";
        $.ajax({
            url: url,
            success: (res) => {
                $("#DisplaySection").html(ToastElement("Total of employee salary is $" + res.totalSalary));
                makeToast(res.totalSalary + " is total salary of all employees.");
            }
        });
    });

    //Get employee who's born before 01-01-2000
    $("#GetEmployeesOlderThanGivenDate").on("click", (e) => {
        let url = "TestTwo/GetEmployeesOlderThanGivenDate";
        $.ajax({
            url: url,
            success: (res) => {
                $("#EmployeeSalaryData").html(res);
            }
        });
    })

    //Get null middlename employees
    $("#GetNullMiddleNameEmployees").on("click", (e) => {
        let url = "TestTwo/GetNullMiddleNameEmployees";
        $.ajax({
            url: url,
            success: (res) => {
                $("#EmployeeSalaryData").html(res);
            }
        });
    })
})