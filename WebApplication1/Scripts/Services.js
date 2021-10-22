//Login Form
$('#btnLogin').click(
    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var Username = $('#username').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Password = $('#password').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}
        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USERNAME = Username;
        obj.PASSWORD = Password;
        $.post({
            url: "/api/Users/authenticateUser",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg').html("<b>" + json.ResponseMessage + "</b>");
                $('#code').html("<b>" + json.StatusCode + "</b>");
                $('#data').html("<b>" + json.Data + "</b>");
                if (json.Data !== null) {
                    $('#info').show();
                    $('#id').html("<b>" + json.Data.USER_ID + "</b>");
                    $('#token').html("<b>" + json.Data.TOKEN + "</b>");
                }
                else {
                    $('#info').hide();
                }
                //=========================================================
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });

//Create Account
$('#btnAccount2').click(
    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var UserId = $('#userid2').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Token = $('#tokenvalue2').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}

        var AccountNo = $('#acctno2').val();
        //if (AccountNo == null) {
        //    alert("Account No is required.")
        //    return;
        //}

        var LastName = $('#lname').val();
        //if (LastName == null) {
        //    alert("Last Name is required.")
        //    return;
        //}

        var FirstName = $('#fname').val();
        //if (FirstName == null) {
        //    alert("First Name is required.")
        //    return;
        //}

        var MiddleName = $('#mname').val();
        var Balance = $('#balance').val();
        //if (Balance == null) {
        //    alert("Initial Balance is required.")
        //    return;
        //}

        var Remarks = $('#remarks').val();
        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USER_ID = UserId;
        obj.TOKEN = Token;
        obj.ACCOUNT_NO = AccountNo;
        obj.LASTNAME = LastName;
        obj.FIRSTNAME = FirstName;
        obj.MIDDLENAME = MiddleName;
        obj.ACCOUNT_BALANCE = Balance;
        obj.REMARKS = Remarks;
        $.post({
            url: "/api/Accounts/create",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg3').html("<b>" + json.ResponseMessage + "</b>");
                $('#code3').html("<b>" + json.StatusCode + "</b>");
                $('#data3').html("<b>" + json.Data + "</b>");
                //=========================================================
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });

//Create Account
$('#btnAccount1').click(
    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var UserId = $('#userid1').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Token = $('#tokenvalue1').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}

        var AccountNo = $('#acctno1').val();
        //if (AccountNo == null) {
        //    alert("Account No is required.")
        //    return;
        //}

        var LastName = $('#lname1').val();
        //if (LastName == null) {
        //    alert("Last Name is required.")
        //    return;
        //}

        var FirstName = $('#fname1').val();
        //if (FirstName == null) {
        //    alert("First Name is required.")
        //    return;
        //}

        var MiddleName = $('#mname1').val();
        var Status = $('#status1').val();
        var Remarks = $('#remarks1').val();
        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USER_ID = UserId;
        obj.TOKEN = Token;
        obj.ACCOUNT_NO = AccountNo;
        obj.LASTNAME = LastName;
        obj.FIRSTNAME = FirstName;
        obj.MIDDLENAME = MiddleName;
        obj.STATUS = Status;
        obj.REMARKS = Remarks;
        $.post({
            url: "/api/Accounts/update",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg1').html("<b>" + json.ResponseMessage + "</b>");
                $('#code1').html("<b>" + json.StatusCode + "</b>");
                $('#data1').html("<b>" + json.Data + "</b>");
                //=========================================================
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });
//Get Account
$('#btnAccount').click(
    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var UserId = $('#userid').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Token = $('#tokenvalue').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}

        var AccountNo = $('#acctno').val();
        //if (AccountNo == null) {
        //    alert("Account No is required.")
        //    return;
        //}
        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USER_ID = UserId;
        obj.TOKEN = Token;
        obj.ACCOUNT_NO = AccountNo;
        $.post({
            url: "/api/Accounts/get",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg2').html("<b>" + json.ResponseMessage + "</b>");
                $('#code2').html("<b>" + json.StatusCode + "</b>");
                $('#data2').html("<b>" + json.Data + "</b>");
                if (json.Data !== null) {
                    $('#info2').show();
                    $('#name').html("<b>" + json.Data.NAME + "</b>");
                    $('#balance2').html("<b>" + json.Data.ACCOUNT_BALANCE + "</b>");
                    $('#status').html("<b>" + json.Data.STATUS + "</b>");
                    $('#remarks2').html("<b>" + json.Data.REMARKS + "</b>");
                }
                else {
                    $('#info2').hide();
                }
                //=========================================================
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });

//Create Payment
$('#btnPayment').click(
    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var UserId = $('#userid4').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Token = $('#tokenvalue4').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}

        var AccountNo = $('#acctno4').val();
        //if (AccountNo == null) {
        //    alert("Account No is required.")
        //    return;
        //}

        var Amount = $('#amount4').val();
        //if (Balance == null) {
        //    alert("Initial Balance is required.")
        //    return;
        //}

        var Remarks = $('#remarks4').val();
        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USER_ID = UserId;
        obj.TOKEN = Token;
        obj.ACCOUNT_NO = AccountNo;
        obj.AMOUNT = Amount;
        obj.REMARKS = Remarks;
        $.post({
            url: "/api/Payments/create",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg4').html("<b>" + json.ResponseMessage + "</b>");
                $('#code4').html("<b>" + json.StatusCode + "</b>");
                $('#data4').html("<b>" + json.Data + "</b>");
                //=========================================================
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });

//Get Payments by Account No.
$('#btnGetPayments').click(

    function () {
        //This validations is optional as API validations are also implemented
        //==================================================================//
        var UserId = $('#userid6').val();
        //if (Username == '') {
        //    alert("Username is required.")
        //    return;
        //}

        var Token = $('#tokenvalue6').val();
        //if (Password == null) {
        //    alert("Password is required.")
        //    return;
        //}

        var AccountNo = $('#acctno6').val();
        //if (AccountNo == null) {
        //    alert("Account No is required.")
        //    return;
        //}

        //=================================================================//

        // Create FormData object
        var obj = new Object();
        obj.USER_ID = UserId;
        obj.TOKEN = Token;
        obj.ACCOUNT_NO = AccountNo;
        $.post({
            url: "/api/Payments/getByAccountNo",
            type: "POST",
            data: obj,
            cache: false,
            success: function (res) {
                //parse json string to object
                var json = $.parseJSON(res);
                //display results==========================================
                $('#msg6').html("<b>" + json.ResponseMessage + "</b>");
                $('#code6').html("<b>" + json.StatusCode + "</b>");
                $('#data6').html("<b>" + json.Data + "</b>");
                //=========================================================

                if (json.Data !== null) {
                    $('#info6').show();
                    var tb = "<table border='1'><tr><th>Date</th><th>Amount</th><th>Remarks</th></tr>";
                    json.Data.forEach(function (element) {
                        tb += "<tr><td>" + element.DATE_FORMAT1 + "</td>";
                        tb += "<td>" + element.AMOUNT + "</td>";
                        tb += "<td>" + element.REMARKS + "</td></tr>";
                    });
                    tb += "</table>";
                    $('#PaymentsHistory').html(tb);
                }
                else {
                    $('#info6').hide();
                    $('#PaymentsHistory').html("");
                }
                //show result in alert
                //alert(res);
            },
            error: function (res, status, errorThrow) {
            },
        });
    });
