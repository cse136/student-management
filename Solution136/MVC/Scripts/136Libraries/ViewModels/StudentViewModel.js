function StudentViewModel() {

    var StudentModelObj = new StudentModel();
    var self = this;
    var initialBind = true;
    var studentListViewModel;

    this.Initialize = function() {

        self.studentListViewModel = {
            id: ko.observable(""),
            ssn: ko.observable(""),
            first: ko.observable(""),
            last: ko.observable(""),
            email: ko.observable(""),
            password: ko.observable(""),
            shoesize: ko.observable(""),
            weight: ko.observable(""),
            gpa: ko.observable(0),

            add: function (data) {
                self.CreateStudent(data);
            },
            
            getall: function () {
            
                self.GetAll();
            },
            
            studentList: ko.observableArray(),
            
            filter: function () {
                debugger;
                StudentModelObj.Load(self.studentListViewModel.id(), self.studentListViewModel.first(), self.studentListViewModel.last(), self.studentListViewModel.email(), self.studentListViewModel.gpa(), function (studentListData) {
                    debugger;
                    self.studentListViewModel.studentList.removeAll();

                    // DTO from the JSON model to the view model. In this case, studentListViewModel doesn't need the "id" attribute
                    for (var i = 0; i < studentListData.length; i++) {
                        self.studentListViewModel.studentList.push({
                            id: studentListData[i].student_id,
                            first: studentListData[i].first_name,
                            last: studentListData[i].last_name,
                            email: studentListData[i].email,
                            shoesize: studentListData[i].shoe_size,
                            weight: studentListData[i].weight
                        });
                    }
                });
            }

        };

        if (initialBind) {
            // this is using knockoutjs to bind the viewModel and the view
            ko.applyBindings({ viewModel: self.studentListViewModel }/*, document.getElementById("student_form")*/);
            initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
        }


      };


    

    this.CreateStudent = function(data) {
        var model = {
            id: data.student_id(),
            ssn: data.ssn(),
            first: data.first_name(),
            last: data.last_name(),
            email: data.email(),
            password: data.password(),
            shoesize: data.shoe_size(),
            weight: data.weight()
        }

        StudentModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create student successful");
            } else {
                alert("Error occurred");
            }
        });
    };

    this.GetDetail = function (id) {

        StudentModelObj.GetDetail(id, function (result) {

            var student = {
                id: result.StudentId,
                first: result.FirstName,
                last: result.LastName,
                email: result.Email,
                shoesize: result.ShoeSize,
                weight: result.Weight,
                ssn: result.SSN
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: student }, document.getElementById("divStudentContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteStudent = {
        init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function() {
                var id = viewModel.id;

                StudentModelObj.Delete(id, function(result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        studentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };

    ko.bindingHandlers.DeleteStudentAsync = {
        init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function() {
                var id = viewModel.id;

                StudentModelObj.DeleteAsync(id, function(result) {
                    alert(result);
                    //studentListViewModel.remove(viewModel);
                });
            });
        }
    };

}