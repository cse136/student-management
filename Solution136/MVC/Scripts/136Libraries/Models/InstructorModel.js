function InstructorModel() {

    this.Filter = function (instructor, title, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Instructor/GetList?instructor=" + instructor + "&title=" + title,
            data: "",
            dataType: "json",
            success: function (instructorData) {
                debugger;
                callback(instructorData);
            },
            error: function () {
                alert('Error while loading instructor list. Is your service layer running?');
            }
        });
    };

    this.Edit = function (model, callback) {
        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Instructor/Edit",
            data: model,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while editing instructor. Is your service layer running?');
            }
        });
    };

    this.GetDetail = function (id, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Instructor/GetDetail?id=" + id,
            data: "",
            dataType: "json",
            success: function (data) {
                callback(data);
            },
            error: function () {
                alert('Error while loading instructor. Is your service layer running?');
            }
        });
    };


    this.Add = function (model, callback) {
        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Instructor/Add",
            data: model,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding instructor. Is your service layer running?');
            }
        });
    };
}
