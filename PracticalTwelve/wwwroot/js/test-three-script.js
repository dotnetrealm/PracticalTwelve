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
    //reloadTable();

    //fetch all users
    $("#TestThreeActions #GetEmployeeCountByDesignationBtn").on("click", () => {
        let url = "TestThree/GetEmployeeCountsByDesignation";
        $("#DisplaySection").load(url);
        makeToast("Data loaded.");
    });

    //fetch employees with designation
    $("#TestThreeActions #GetEmployeeDesignationDetailsBtn").on("click", () => {
        let url = "TestThree/GetEmployeeDesignationDetails";
        $("#DisplaySection").load(url);
        makeToast("Employee with designation  data loaded.");
    });

    //Designations that have more than one employee
    $("#TestThreeActions #GetDesignationThatHaveMoreThanOneEmployeeBtn").on("click", () => {
        let url = "TestThree/GetDesignationThatHaveMoreThanOneEmployee";
        $("#DisplaySection").load(url);
        makeToast("Data loaded successfully.");
    });

    //get Employee have max salary
    $("#TestThreeActions #GetEmployeeHavingMaxSalaryBtn").on("click", () => {
        let url = "TestThree/GetEmployeeHavingMaxSalary";
        $("#DisplaySection").load(url);
        makeToast("Data loaded successfully.");
    });
})