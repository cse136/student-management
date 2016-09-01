function CourseListModel() {

    this.Load = function (course, level, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Course/GetCourseList?course=" + course + "&level=" + level,
            data: "",
            dataType: "json",
            success: function (courseListData) {
                debugger;
                callback(courseListData);
            },
            error: function () {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };
}
