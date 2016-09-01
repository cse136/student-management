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

            display_name: ko.observable(''),

            //course: ko.observable(''),
            //course_title: ko.observable(''),


            availableTitles: ko.observableArray([
                new Title('All', ''),
                new Title('Research Instructor', 'Research Instructor'),
                new Title('Research Assistant Professor', 'Research Assistant Professor'),
                new Title('Research Associate Professor', 'Research Associate Professor'),
                new Title('Research Professor', 'Research Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Assistant Professor'),
                new Title('Adjunct Faculty', 'Adjunct Faculty'),

                new Title('Adjunct Associate Professor', 'Adjunct Associate Professor'),
                new Title('Adjunct Professor', 'Adjunct Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Assistant Professor'),

                new Title('Visiting Associate Professor', 'Visiting Associate Professor'),
                new Title('Visiting Professor', 'Visiting Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Associate Professo'),
                new Title('Visiting Professor', 'Visiting Professor')


            ]),

            availableTitlesWithoutAll: ko.observableArray([
                new Title('Research Instructor', 'Research Instructor'),
                new Title('Research Assistant Professor', 'Research Assistant Professor'),
                new Title('Research Associate Professor', 'Research Associate Professor'),
                new Title('Research Professor', 'Research Professor'),
                new Title('Adjunct Assistant Professor', 'Adjunct Assistant Professor'),
                new Title('Adjunct Faculty', 'Adjunct Faculty'),

                new Title('Adjunct Associate Professor', 'Adjunct Associate Professor'),
                new Title('Adjunct Professor', 'Adjunct Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Assistant Professor'),

                new Title('Visiting Associate Professor', 'Visiting Associate Professor'),
                new Title('Visiting Professor', 'Visiting Professor'),
                new Title('Visiting Assistant Professor', 'Visiting Associate Professo'),
                new Title('Visiting Professor', 'Visiting Professor')
            ]),

            selectedTitle: ko.observable(''), // all selected by default,

            name_filter: ko.observable(''),

            first: ko.observable(''),
            last: ko.observable(''),


            instructorList: ko.observableArray(),

            getDetail: function (id) {
                instructorModelObj.GetDetail(id,
                   function (data) {
                       self.instructorViewModel.first(data.first_name);
                       self.instructorViewModel.last(data.last_name);

                       self.instructorViewModel.display_name(data.first_name + ' ' + data.last_name);
                       debugger;
                       self.instructorViewModel.selectedTitle(
                           self.instructorViewModel.availableTitlesWithoutAll().find(
                            function (title) {
                                return title.value == data.title;
                            }));


                   });
            },


            filter: function () {

                instructorModelObj.Filter(self.instructorViewModel.name_filter(),
                    self.instructorViewModel.selectedTitle().value,
                    function (instructorData) {

                        self.instructorViewModel.instructorList.removeAll();

                        // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                        for (var i = 0; i < instructorData.length; i++) {
                            self.instructorViewModel.instructorList.push(
                                {
                                    title: instructorData[i].title,
                                    name: instructorData[i].first_name + ' ' + instructorData[i].last_name,
                                    id: instructorData[i].instructor_id
                                }
                            );
                        }
                    });
            },


            edit: function (id, data) {
                debugger;
                var model = {
                    instructor_id: id,
                    first_name: data.viewModel.first(),
                    last_name: data.viewModel.last(),
                    title: data.viewModel.selectedTitle().value
                };

                instructorModelObj.Edit(model, function (result) {
                    if (result == "ok") {


                        alert("Edited instructor successful");


                    } else {
                        alert("Error occurred");
                    }
                });

            },

            add: function (data) {
                debugger;
                var model = {
                    instructor_id: 0,
                    first_name: data.viewModel.first(),
                    last_name: data.viewModel.last(),
                    title: data.viewModel.selectedTitle().value
                };

                instructorModelObj.Add(model, function (result) {
                    if (result == "ok") {
                        debugger;

                        alert("Added instructor successful");

                        window.location.href = '/Staff/InstructorList'; //relative to domain


                    } else {
                        alert("Error occurred");
                    }
                });

            }

        };


        if (initialBind) {
            // this is using knockoutjs to bind the viewModel and the view
            ko.applyBindings({ viewModel: self.instructorViewModel });
            initialBind = false;
        }
    };
};