function CourseListViewModel() {

    var courseListModelObj = new CourseListModel();
    var self = this;
    var initialBind = true;
    var courseListViewModel;

    this.Initialize = function () {
        

        // Constructor for object
        var Level = function (displayName, value) {
            this.displayName = displayName;
            this.value = value;
        };

        self.courseListViewModel = {
            
            availableLevels: ko.observableArray([
                new Level('All', ''),
                new Level('Upper', 'upper'),
                new Level('Lower', 'lower'),
                new Level('Grad', 'grad')
            ]),

            selectedLevel: ko.observable(''), // all selected by default,

            course: ko.observable(''),

            courseList: ko.observableArray(),

            filter: function () {
                self.Load(self.courseListViewModel.course(), self.courseListViewModel.selectedLevel().value);
            }
        };


        if (initialBind) {
            // this is using knockoutjs to bind the viewModel and the view
            ko.applyBindings({ viewModel: self.courseListViewModel }, document.getElementById("course_list"));
            initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            self.courseListViewModel.filter();
        }
    };

    this.Load = function (course, level) {
        
        courseListModelObj.Load(course, level, function (courseListData) {
            
            self.courseListViewModel.courseList.removeAll();

            // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
            for (var i = 0; i < courseListData.length; i++) {
                self.courseListViewModel.courseList.push({
                    title: courseListData[i].course_title,
                    description: courseListData[i].course_description,
                    level: courseListData[i].course_level,
                    id: courseListData[i].course_id
                });
            }
        });
    };


}
