﻿// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using OtripleS.Web.Api.Models.SemesterCourses.Exceptions;
using OtripleS.Web.Api.Models.StudentSemesterCourses;
using OtripleS.Web.Api.Models.StudentSemesterCourses.Exceptions;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.StudentSemesterCourseServiceTests
{
    public partial class StudentSemesterCourseServiceTests
    {
        [Fact]
        public async void ShouldThrowValidationExceptionOnModifyWhenSemesterCourseIsNullAndLogItAsync()
        {
            //given
            StudentSemesterCourse invalidStudentSemesterCourse = null;
            var nullStudentSemesterCourseException = new NullStudentSemesterCourseException();

            var expectedSemesterCourseValidationException =
                new SemesterCourseValidationException(nullStudentSemesterCourseException);

            //when
            ValueTask<StudentSemesterCourse> modifyStudentSemesterCourseTask =
                this.studentSemesterCourseService.ModifyStudentSemesterCourseAsync(invalidStudentSemesterCourse);

            //then
            await Assert.ThrowsAsync<StudentSemesterCourseValidationException>(() =>
                modifyStudentSemesterCourseTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedSemesterCourseValidationException))),
                Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
