function TAViewModel() {

    var taModelObj = new TAModel();
    var self = this;
    var initialBind = true;
    var taViewModel;

    // Constructor for object
    var Level = function (displayName, value) {
        this.displayName = displayName;
        this.value = value;
    };

    this.Initialize = function (id) {
        self.taViewModel = {

            id: ko.observable(""),
            type: ko.observable(""),
            first: ko.observable(""),
            last: ko.observable(""),
            
            availableLevels: ko.observableArray([
                new Level('All', ''),
                new Level('Upper', 'upper'),
                new Level('Lower', 'lower'),
                new Level('Grad', 'grad')
            ]),

            availableLevelsWithoutAll: ko.observableArray([
                   new Level('Upper', 'upper'),
                   new Level('Lower', 'lower'),
                   new Level('Grad', 'grad')
            ]),

            selectedLevel: ko.observable(''), // all selected by default

            ta_filter: ko.observable(''),

            taList: ko.observableArray(),

            filter: function () {
                viewModelObj.Filter(self.taViewModel.ta_filter(),
                    self.taViewModel.selectedLevel().value,
                    function (taListData) {

                        self.taViewModel.taList.removeAll();

                        // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                        for (var i = 0; i < taListData.length; i++) {
                            self.taViewModel.taList.push(
                                {
                                    id: taListData[i].ta_id,
                                    type: taListData[i].ta_type_id,
                                    first: taListData[i].first_name,
                                    last: taListData[i].last_name
                                }
                            );
                        }
                    });
            },

            add: function (data) {
                var model = {
                    id: data.viewModel.ta_id(),
                    type: data.viewModel.ta_type_id(),
                    first: data.viewModel.first_name(),
                    last: data.viewModel.last_name()
                };

                courseModelObj.Add(model, function (result) {
                    if (result == "ok") {
                        debugger;

                        alert("Added TA successfully");

                        window.location.href = '/Staff/TAList'; //relative to domain


                    } else {
                        alert("Error occurred");
                    }
                });

            }
        };

        ko.applyBindings(viewModel);
    };

    this.CreateTA = function (data) {
        var model = {
            id: data.ta_id(),
            type: data.ta_type_id(),
            first: data.first(),
            last: data.last()
        }

        TAModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create ta successful");
            } else {
                alert("Error occurred");
            }
        });
    };

    this.GetAll = function () {
        TAModelObj.GetAll(function (taList) {
            taListViewModel.removeAll();

            for (var i = 0; i < taList.length; i++) {
                taListViewModel.push({
                    id: taList[i].ta_id,
                    type: taList[i].type,
                    name: taList[i].first_name + ' ' + taList[i].last_name,
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: taListViewModel }, document.getElementById("TAs"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
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
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.Delete(id, function (result) {
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
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.DeleteAsync(id, function (result) {
                    alert(result);
                    //studentListViewModel.remove(viewModel);
                });
            });
        }
    };

}
