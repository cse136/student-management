function StudentModel(asyncIndicator) {

    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (student, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Student/InsertStudent",
            data: student,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding student.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Student/DeleteStudent?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing student.  Is your service layer running?');
            }
        });
    };

    this.DeleteAsync = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Student/DeleteStudentAsync?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing student.  Is your service layer running?');
            }
        });
    };

    this.GetAll = function (callback) {
        var url = "http://localhost:9393/Api/Student/GetStudentList?bust=" + new Date();
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading student list.  Is your service layer running?');
            }
        });
    };

    this.GetDetail = function (id, callback) {
        var url = "http://localhost:9393/Api/Student/GetStudent?id=" + id + "&bust=" + new Date();
            
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading student detail.  Is your service layer running?');
            }
        });
    };

    this.Load = function (id, first, last, email, gpa, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Student/GetStudentList?id=" + id + "&first=" + first + "&last=" + last + "&email=" + email + "&gpa=" + gpa,
            data: "",
            dataType: "json",
            success: function (studentData) {
                callback(studentData);
            },
            error: function () {
                alert('Error while loading student list.  Is your service layer running?');
            }
        });
    };
}
