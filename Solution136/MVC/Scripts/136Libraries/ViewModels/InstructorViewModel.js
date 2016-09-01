function InstructorViewModel() {

    var instructorModelObj = new InstructorModel();
    var self = this;
    var initialBind = true;
    var instructorViewModel;

    // Constructor for object
    var Title = function (displayName, value) {
        this.displayName = displayName;
        this.value = value;
    };

    this.Initialize = function (id) {

        self.instructorViewModel = {

            course_description: ko.observable(''),

            course: ko.observable(''),
            course_title: ko.observable(''),

            prereqs: ko.observableArray(),

            availableTitles: ko.observableArray([
                new Title('All', ''),
                new Title('Research Instructor', 'Research Instructor'),
                new Title('Research Assistant Professor', 'Research Assistant Professor'),
                new Title('Research Associate Professor', 'Research Associate Professor'),
                new Title('Research Professor', 'Research Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Assistant Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Faculty'),

                new Title('Adjunct Associate Professor', 'Adjunct Associate Professor'),
                new Title('Adjunct Professor', 'Adjunct Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Assistant Professor'),

                new Title('Visiting Associate Professor', 'Visiting Associate Professor'),
                new Title('Visiting Professor', 'Visiting Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Associate Professo'),
                new Title('Visiting Professor', 'Visiting Professor'),


            ]),

            availableTitlesWithoutAll: ko.observableArray([
                new Title('Research Instructor', 'Research Instructor'),
                new Title('Research Assistant Professor', 'Research Assistant Professor'),
                new Title('Research Associate Professor', 'Research Associate Professor'),
                new Title('Research Professor', 'Research Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Assistant Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Faculty'),

                new Title('Adjunct Associate Professor', 'Adjunct Associate Professor'),
                new Title('Adjunct Professor', 'Adjunct Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Assistant Professor'),

                new Title('Visiting Associate Professor', 'Visiting Associate Professor'),
                new Title('Visiting Professor', 'Visiting Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Associate Professo'),
                new Title('Visiting Professor', 'Visiting Professor'),
            ]),

            selectedLevel: ko.observable(''), // all selected by default,

            course_filter: ko.observable(''),

            courseList: ko.observableArray(),

            addPrereq: function (id, prereq) {

                courseModelObj.AddPrereq(id,
                    prereq.id,
                    function (result) {

                        if (result == "ok") {
                            alert("Added pre-requisite successfully");
                            window.location.href = '/Schedule/CourseDetails/' + id; //relative to domain

                        } else if (result == "duplicate") {
                            alert("Pre-requisite already added")
                        }
                        else {
                            alert("Error occurred");
                        }
                    });
            },

            filter: function () {

                courseModelObj.Filter(self.courseViewModel.course_filter(),
                    self.courseViewModel.selectedLevel().value,
                    function (courseListData) {

                        self.courseViewModel.courseList.removeAll();

                        // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                        for (var i = 0; i < courseListData.length; i++) {
                            self.courseViewModel.courseList.push(
                                {
                                    title: courseListData[i].course_title,
                                    description: courseListData[i].course_description,
                                    level: courseListData[i].course_level,
                                    id: courseListData[i].course_id
                                }
                            );
                        }
                    });
            },

            removePrereq: function (id, prereq) {


                courseModelObj.RemovePrereq(id,
                    prereq.id,
                    function (result) {



                        if (result == "ok") {
                            self.courseViewModel.getDetail(id);

                            setTimeout(function () {

                                alert("Removed pre-requisiste successfully");

                            }, 100);
                        } else {
                            alert("Error occurred");
                        }



                    });
            },

            getDetail: function (id) {
                courseModelObj.GetDetail(id,
                   function (courseData) {

                       self.courseViewModel.prereqs.removeAll();
                       self.courseViewModel.course(courseData.course_title);
                       self.courseViewModel.course_title(courseData.course_title);

                       self.courseViewModel.course_description(courseData.course_description);
                       debugger;
                       self.courseViewModel.selectedLevel(
                           self.courseViewModel.availableLevelsWithoutAll().find(
                            function (level) {
                                return level.value == courseData.course_level;
                            }));

                       debugger;
                       // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                       for (var i = 0; i < courseData.prereqs.length; i++) {
                           debugger;
                           self.courseViewModel.prereqs.push(
                               {
                                   title: courseData.prereqs[i].course_title,
                                   description: courseData.prereqs[i].course_description,
                                   level: courseData.prereqs[i].course_level,
                                   id: courseData.prereqs[i].course_id
                               }
                           );
                       }
                   });
            },

            edit: function (id, data) {
                debugger;
                var model = {
                    course_id: id,
                    course_title: data.viewModel.course(),
                    course_description: data.viewModel.course_description(),
                    course_level: data.viewModel.selectedLevel().value
                };

                courseModelObj.Edit(model, function (result) {
                    if (result == "ok") {
                        debugger;
                        data.viewModel.course_title(data.viewModel.course());

                        setTimeout(function () {

                            alert("Edited course successful");


                        }, 100);


                    } else {
                        alert("Error occurred");
                    }
                });

            },

            add: function (data) {
                debugger;
                var model = {
                    course_id: 0,
                    course_title: data.viewModel.course(),
                    course_description: data.viewModel.course_description(),
                    course_level: data.viewModel.selectedLevel().value
                };

                courseModelObj.Add(model, function (result) {
                    if (result == "ok") {
                        debugger;

                        alert("Added course successful");

                        window.location.href = '/Schedule/CourseList'; //relative to domain


                    } else {
                        alert("Error occurred");
                    }
                });

            }

        };


        if (initialBind) {
            // this is using knockoutjs to bind the viewModel and the view
            ko.applyBindings({ viewModel: self.courseViewModel }, document.getElementById("courseViewModel"));
            initialBind = false;
        }
    };
};