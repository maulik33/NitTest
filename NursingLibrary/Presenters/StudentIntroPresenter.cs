using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
	public class StudentIntroPresenter : StudentPresenter<IStudentIntroView>
	{
		#region Fields

		private readonly IDictionary<int, IEnumerable<Question>> testQuestions = new Dictionary<int, IEnumerable<Question>>();
		private readonly IDictionary<int, IEnumerable<Question>> testQuestionsItems = new Dictionary<int, IEnumerable<Question>>();

		private IEnumerable<UserTest> _suspendedTests;
		private IEnumerable<Question> _testQuestionsById;
		private IEnumerable<Question> _userTestQuestionsById;

		#endregion Fields

		#region Constructors

		public StudentIntroPresenter(IStudentAppController appController)
			: base(appController)
		{
		}

		#endregion Constructors

		#region Properties

		public TraceHelper CurrentTrace { get; set; }

		public IEnumerable<UserTest> SuspendedTests
		{
			get { return _suspendedTests ?? (_suspendedTests = AppController.GetUserTestByID()); }
		}

		public IEnumerable<Question> TestQuestionsById
		{
			get
			{
				if (_testQuestionsById == null || _testQuestionsById.Count() == 0)
				{
					_testQuestionsById = AppController.GetQuestionByTest();
				}

				return _testQuestionsById;
			}
		}

		public IEnumerable<Question> UserTestQuestionsById
		{
			get
			{
				if (_userTestQuestionsById == null || _userTestQuestionsById.Count() == 0)
				{
					_userTestQuestionsById = AppController.GetQuestionByUserTest();
				}

				return _userTestQuestionsById;
			}
		}

		#endregion Properties

		#region Methods

		public virtual void GotoReviewPage()
		{
			TraceHelper.WriteTraceEvent(TraceToken, "Intro Presenter.GotoReviewPage");
			if (Student.Action == Action.Review)
			{
				AppController.ShowPage(PageDirectory.Review, null, null);
			}
			else
			{
				if (WriteQuestionInTheTest())
				{
					if (Student.TestType == TestType.Integrated || Student.TestType == TestType.FocusedReview)
					{
						AppController.ShowPage(PageDirectory.TestReview, null, null);
					}
					else if (Student.TestType == TestType.SkillsModules)
					{
						AppController.ShowPage(PageDirectory.SkillsModules, null, null);
					}
					else
					{
						AppController.ShowPage(
							Student.QuizOrQBank == TestType.Quiz ? PageDirectory.TestReview : PageDirectory.QbankR, null,
							null);
					}
				}
			}
		}

		public virtual void OnBackClick()
		{
			if (Student.Action == Action.Review)
			{
				AppController.UpdateQuestionExplanation(View.Timer);
			}

			if (Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
			}

			TakeNextPrevQuestion(Convert.ToInt32(View.QuestionNumber), (QuestionFileType)Enum.Parse(typeof(QuestionFileType), View.FileType), "P");
		}

		public virtual void OnAltTabClick()
		{
			if (Student.Action == Action.Review)
			{
				AppController.UpdateQuestionExplanation(View.Timer);
			}

			if (Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
			}

			if (View.TabIndex == 1)
			{
				AppController.UpdateAltTabClick(Student.UserTestId, Student.QuestionId, true);
			}

			TakeNextPrevQuestion(Convert.ToInt32(Student.QuestionId), (QuestionFileType)Enum.Parse(typeof(QuestionFileType), View.FileType), "C");
		}

		public virtual void OnBackIncorrectClick()
		{
			if (Student.Action == Action.Review)
			{
				AppController.UpdateQuestionExplanation(View.Timer);
			}

			if (Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
			}

			ViewNextPreviewIncorrectQuestion(Convert.ToInt32(View.QuestionNumber), (QuestionFileType)Enum.Parse(typeof(QuestionFileType), View.FileType), "P");
		}

		public virtual void OnIbIntroDClick()
		{
			TakeNextPrevQuestion(0, QuestionFileType.Intro, "N");
		}

		public virtual void OnIbIntroSClick()
		{
			var fileType = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), View.FileType);
			if (fileType != QuestionFileType.EndItem)
			{
				TakeNextPrevQuestion(0, QuestionFileType.TutorialItem, "N");
			}
			else if (fileType == QuestionFileType.Disclaimer)
			{
				TakeNextPrevQuestion(0, QuestionFileType.Intro, "N");
			}
			else if (fileType == QuestionFileType.EndItem)
			{
				if ((Student.ProductId == 3 || Student.ProductId == 6) && Student.QuizOrQBank == TestType.Qbank)
				{
					AppController.ShowPage(PageDirectory.ListReview, null, null);
				}
				else
				{
					AppController.ShowPage(Student.QuizOrQBank == TestType.Quiz ? PageDirectory.ListReview : PageDirectory.QbankR, null, null);
				}
			}
			else
			{
				GotoReviewPage();
			}
		}

		public virtual void OnIbSkipClick()
		{
			TakeNextPrevQuestion(0, QuestionFileType.Question, "N");
		}

		public virtual void OnNextClick(bool visible)
		{
			bool canGoToNextQuestion;
			var fileType = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), View.FileType);
			if (Student.Action != Action.Review)
			{
				if (Student.Action == Action.Remediation)
				{
					canGoToNextQuestion = true;
				}
				else
				{
					canGoToNextQuestion = fileType == QuestionFileType.Question ? WriteQuestionInTheTest() : AppController.UpdateEndTest(View.UserTest);
				}
			}
			else
			{
				canGoToNextQuestion = true;
			}

			if (canGoToNextQuestion)
			{
				if (fileType == QuestionFileType.TutorialItem)
				{
					TakeNextPrevQuestion(Convert.ToInt32(View.QuestionNumber), QuestionFileType.TutorialItem, "N");
				}
				else
				{
					if (Student.Action == Action.Review)
					{
						AppController.UpdateQuestionExplanation(View.Timer);
					}

					if (Student.Action == Action.Remediation)
					{
						AppController.UpdateQuestionRemediation(View.Timer);
					}

					TakeNextPrevQuestion(Convert.ToInt32(View.QuestionNumber), QuestionFileType.Question, "N");
				}
			}

			if (View.IsNextVisible == false && Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
				AppController.ShowPage(Student.QuizOrQBank == TestType.Quiz ? PageDirectory.Review : PageDirectory.QbankR, null, null);
			}
		}

		public virtual void OnNextIncorrectClick(bool visible)
		{
			var canGoToNextQuestion = false;
			if (Student.Action != Action.Review)
			{
				if (Student.Action == Action.Remediation)
				{
					canGoToNextQuestion = true;
				}
			}
			else
			{
				canGoToNextQuestion = true;
			}

			if (canGoToNextQuestion)
			{
				if (Student.Action == Action.Review)
				{
					AppController.UpdateQuestionExplanation(View.Timer);
				}

				if (Student.Action == Action.Remediation)
				{
					AppController.UpdateQuestionRemediation(View.Timer);
				}

				ViewNextPreviewIncorrectQuestion(Convert.ToInt32(View.QuestionNumber), QuestionFileType.Question, "N");
			}

			if (View.IsNextVisible == false && Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
				AppController.ShowPage(
					Student.QuizOrQBank == TestType.Quiz ? PageDirectory.ListReview : PageDirectory.QbankR, null, null);
			}
		}

		public virtual void OnQuitClick()
		{
			switch (Student.Action)
			{
				case Action.Review:
					AppController.UpdateQuestionExplanation(View.Timer);
					GotoReviewPage();
					break;
				case Action.Remediation:
					AppController.UpdateQuestionRemediation(View.Timer);
					AppController.ShowPage(Student.QuizOrQBank == TestType.Quiz ? PageDirectory.Review : PageDirectory.QbankR, null, null);
					break;
				default:
					AppController.UpdateEndTest(View.UserTest);
					GotoReviewPage();
					break;
			}
		}

		public override void OnViewInitialized()
		{
			base.OnViewInitialized();
			View.Resume = false;
			View.EnableLabel = false;
			Intialize();
			View.SetControls();
			TraceHelper.Create(TraceToken, "Initializing Test")
				.Add("Test Id", Student.TestId.ToString())
				.Add("User Test Id", Student.UserTestId.ToString())
				.Write();
			var productTest = AppController.GetAllProductTests().FirstOrDefault(tst => tst.TestId == Student.TestId);
			if (productTest == null)
			{
				throw new InvalidOperationException(string.Format("TestId was invalid or not provided for student: {0}", Student.UserId));
			}
			View.TestName = productTest.TestName;
			View.ADA = Student.Ada.ToString();
			View.SecondPerQuestion = productTest.SecondPerQuestion.ToString();
			View.TestType = Student.ProductId.ToString();
			if (Student.Action == Action.NewTest)
			{
				if (Student.TestType == TestType.Integrated)
				{
					var clientAllowed = string.IsNullOrEmpty(View.HTTP_X_FORWARDED_FOR)
									   ? ValidateIpLock(new[] { View.UserHostAddress }, Student.InstitutionIpLock)
									   : ValidateIpLock(new[] { View.HTTP_X_FORWARDED_FOR }, Student.InstitutionIpLock);

					if (!clientAllowed)
					{
						AppController.ShowPage(PageDirectory.AccessDenied, null, null);
					}
				}

				OnNewTest();
			}

			if (Student.Action == Action.Review)
			{
				int qid = Student.QuestionId;
				View.SetRemediationCtrl(Student.UserTestId, Action.Review.ToString());
				var ftype = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), Student.SuspendType);
				Student.QuestionId = 0;
				TakeNextPrevQuestion(qid, ftype, "C");
				if (Student.ProductId == 3)
				{
					View.SetTabVisibility = true;
					View.CheckIsFocused = false;
				}
				else
				{
					View.SetTabVisibility = false;
					View.CheckIsFocused = true;
				}
			}

			if (Student.Action == Action.Remediation)
			{
				View.SetRemediationCtrl(Student.UserTestId, Action.Remediation.ToString());
				TakeNextPrevQuestion(Student.QuestionId, QuestionFileType.Unknown, "C");
				Student.QuestionType = QuestionType.Unknown;
				View.SetTabVisibility = false;
				if (Student.ProductId == 3)
				{
					View.SetTabVisibility = true;
					View.CheckIsFocused = false;
				}
				else
				{
					View.SetTabVisibility = false;
					View.CheckIsFocused = true;
				}
			}

			if (Student.Action == Action.QBankCreate)
			{
				View.Resume = true;
				if (SuspendedTests.Count() > 0)
				{
					var suspendedTest = SuspendedTests.FirstOrDefault();
					View.SetQBankCreateCtrl(suspendedTest);
					View.TimedTestQB = suspendedTest.TimedTest.ToString();
					var ftype = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), suspendedTest.SuspendType);
					TakeNextPrevQuestion(suspendedTest.SuspendQID, ftype, "N");
				}
			}

			if (Student.Action == Action.Rejoin || Student.Action == Action.Resume)
			{
				if (Student.TestType == TestType.Integrated)
				{
					var clientAllowed = string.IsNullOrEmpty(View.HTTP_X_FORWARDED_FOR)
									   ? ValidateIpLock(new[] { View.UserHostAddress }, Student.InstitutionIpLock)
									   : ValidateIpLock(new[] { View.HTTP_X_FORWARDED_FOR }, Student.InstitutionIpLock);

					if (!clientAllowed)
					{
						AppController.ShowPage(PageDirectory.AccessDenied, null, null);
					}
				}

				View.Resume = true;
				if (SuspendedTests.Count() > 0)
				{
					var suspendedTest = SuspendedTests.FirstOrDefault();
					View.SetRejoinResumeCtrl(suspendedTest);
					View.TimedTestQB = suspendedTest.TimedTest.ToString();
					var ftype = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), suspendedTest.SuspendType);
					TakeNextPrevQuestion(suspendedTest.SuspendQID, ftype, "C");
				}
			}

			TraceHelper.Create(TraceToken, "View Initialized.")
				.Add("Test Name", productTest.TestName)
				.Add("Timer", View.Timer.ToString())
				.Add("Timer Count", View.TimerCount.ToString())
				.Write();
		}

		public override void OnViewLoaded()
		{
			TraceHelper.WriteTraceEvent(TraceToken, "StudentIntroPresenter.On View Loaded Begin");
			base.OnViewLoaded();
			Intialize();
			View.SetControls();
			if (View.Postback)
			{
				View.EnableLabel = false;
				var answers = AppController.GetAnswersForQuestion();
				var answersById = AppController.GetUserAnswerByID();
				answersById = answersById.Where(ans => ans.AType == 1).OrderBy(key => key.AIndex).ToArray();
				switch (Student.QuestionType)
				{
					case QuestionType.MultiChoiceSingleAnswer:
						{
							Student.QuestionType = QuestionType.MultiChoiceSingleAnswer;
							View.FillMultipleChoiceFields(answersById,
														  AppController.GetQuestionExhibitByID(), answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.MultiChoiceMultiAnswer:
						{
							Student.QuestionType = QuestionType.MultiChoiceMultiAnswer;
							View.FillMultipleChoiceMultiSelectFields(answersById,
																	 AppController.GetQuestionExhibitByID(),
																	 answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.Hotspot:
						{
							Student.QuestionType = QuestionType.Hotspot;
							var hotSpotAnswers = AppController.GetHotSpotAnswerByID();
							hotSpotAnswers =
								hotSpotAnswers.Where(ans => ans.AType == 1).OrderBy(key => key.AIndex);
							View.FillHotSpotFields(answers,
												   UserTestQuestionsById.FirstOrDefault(
													   qst => qst.Id == Student.QuestionId), hotSpotAnswers);
							View.PopulateAlternateTextDetails(answers, UserTestQuestionsById.FirstOrDefault(
													   qst => qst.Id == Student.QuestionId), hotSpotAnswers, Student);
						}

						break;
					case QuestionType.Number:
						{
							Student.QuestionType = QuestionType.Number;
							View.FillTheBlankFields(answersById, answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.Order:
						{
							Student.QuestionType = QuestionType.Order;
							View.FillTheMatchFields(answersById,
													UserTestQuestionsById.FirstOrDefault(
														qst => qst.Id == Student.QuestionId));
							View.PopulateAlternateTextDetails(answersById, UserTestQuestionsById.FirstOrDefault(
														qst => qst.Id == Student.QuestionId), Student);
						}

						break;
				}
			}

			if (Student.Action == Action.Review)
			{
				if (View.Timer != 0)
				{
					AppController.UpdateQuestionExplanation(View.Timer);
				}

				View.NextInCorrectButton = true;
				if (Student.ProductId == 3)
				{
					View.SetTabVisibility = true;
					View.CheckIsFocused = false;
				}
				else
				{
					View.SetTabVisibility = false;
					View.CheckIsFocused = true;
				}
			}

			if (Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
				View.NextInCorrectButton = true;
				if (Student.ProductId == 3)
				{
					View.SetTabVisibility = true;
					View.CheckIsFocused = false;
				}
				else
				{
					View.SetTabVisibility = false;
					View.CheckIsFocused = true;
				}
			}

			TraceHelper.WriteTraceEvent(TraceToken, "StudentIntroPresenter.On View Loaded End");
		}

		public virtual void UpdateQuestionRemediation(string timerValue, string qid, string userTestId, string action)
		{
			try
			{
				// This throws error sometimes. 
				// To be investigated: Is Session timing out? If so other parts of this method could also fail.
				TraceHelper.Create(TraceToken, "Save Remediation Time")
				.Add("User Test Id", userTestId)
				.Add("Question Id", qid)
				.Add("Timer", timerValue)
				.Write();
			}
			catch
			{
			}

			Student.UserTestId = Convert.ToInt32(userTestId);
			Student.QuestionId = Convert.ToInt32(qid);
			var timeforRemediation = 0;
			var time = timerValue.Trim().Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt32(t));
			switch (time.Count())
			{
				case 3:
					timeforRemediation = (int)new TimeSpan(time.ElementAtOrDefault(0), time.ElementAtOrDefault(1),
												time.ElementAtOrDefault(2)).TotalSeconds;
					break;
				case 2:
					timeforRemediation =
						(int)new TimeSpan(0, time.ElementAtOrDefault(0), time.ElementAtOrDefault(1)).TotalSeconds;
					break;
				case 1:
					timeforRemediation = (int)new TimeSpan(0, 0, time.ElementAtOrDefault(0)).TotalSeconds;
					break;
			}

			if (action == Action.Remediation.ToString())
			{
				AppController.UpdateQuestionRemediation(timeforRemediation);
			}
			else if (action == Action.Review.ToString())
			{
				AppController.UpdateQuestionExplanation(timeforRemediation);
			}
		}

		public void UpdateRemediation()
		{
			if (Student.Action == Action.Remediation)
			{
				AppController.UpdateQuestionRemediation(View.Timer);
			}
			else if (Student.Action == Action.Review)
			{
				AppController.UpdateQuestionExplanation(View.Timer);
			}
		}

		public void PopulateAlternateTextDetails(Student Student)
		{
			var answersById = AppController.GetUserAnswerByID();
			answersById = answersById.Where(ans => ans.AType == 1).OrderBy(key => key.AIndex).ToArray();
			var answers = AppController.GetAnswersForQuestion();
			View.PopulateAlternateTextDetails(answersById, answers, Student);
		}

		public void SetSkillsModuleId()
		{
			Student.SMUserId = AppController.GetSkillModuleUserId(Student.TestId, Student.UserId);
		}

		public int GetOriginalSMTestId()
		{
			return AppController.GetOriginalSMTestId(Student.TestId, Student.UserId);
		}

		public IEnumerable<Test> GetUnTakenTests()
		{
			return AppController.GetUnTakenTests();
		}

		private IEnumerable<Question> GetNextPrevQuestions(int number, QuestionFileType ftype, bool inCorrectOnly)
		{
			IEnumerable<Question> questions;
			if (testQuestions.ContainsKey(number))
			{
				questions = testQuestions[number];
			}
			else
			{
				questions =
					AppController.GetPrevNextQuestions(number, ftype, inCorrectOnly).OrderBy(key => key.FileType).ThenByDescending(
						key => key.QuestionNumber);
				testQuestions.Add(number, questions);
			}

			return questions;
		}

		private IEnumerable<Question> GetPrevNextQuestionInTest(int number, QuestionFileType ftype)
		{
			IEnumerable<Question> questions;
			if (testQuestionsItems.ContainsKey(number))
			{
				questions = testQuestionsItems[number];
			}
			else
			{
				questions =
					AppController.GetPrevNextQuestionInTest(number, ftype).OrderBy(key => key.FileType).ThenByDescending(
						key => key.QuestionNumber);
				testQuestionsItems.Add(number, questions);
			}

			return questions;
		}

		private void Intialize()
		{
			if (Student.Action == Action.Undefined)
			{
				AppController.ShowPage(PageDirectory.StudentLogin, null, null);
			}

			if (Student.ProductId == -1)
			{
				AppController.ShowPage(PageDirectory.StudentLogin, null, null);
			}

			if (Student.TestType == TestType.Undefined)
			{
				AppController.ShowPage(PageDirectory.StudentLogin, null, null);
			}
		}

		private void NextPrevQuestions(int number, QuestionFileType ftype, string action)
		{
			IEnumerable<Question> questions = new List<Question>().ToArray();
			switch (ftype)
			{
				case QuestionFileType.EndItem:
				case QuestionFileType.Unknown:
					GotoReviewPage();
					break;
				case QuestionFileType.Disclaimer:
					TakeNextPrevQuestion(0, QuestionFileType.Intro, "N");
					break;
				case QuestionFileType.Intro:
					TakeNextPrevQuestion(0, QuestionFileType.TutorialItem, "N");
					break;
				case QuestionFileType.TutorialItem:
					{
						if (action == "N")
						{
							if (Student.Action != Action.Review)
							{
								questions =
									GetNextPrevQuestions(0, QuestionFileType.Question, false).OrderBy(key => key.FileType).ThenByDescending(key => key.QuestionNumber).Where(
										   qst => qst.QuestionNumber > 0);
							}
							else
							{
								GotoReviewPage();
							}
						}
						else
						{
							questions =
								GetPrevNextQuestionInTest(number, QuestionFileType.TutorialItem).OrderBy(key => key.FileType).ThenByDescending(key => key.QuestionNumber).Where(
									qst =>
									qst.FileType == QuestionFileType.TutorialItem && qst.QuestionNumber < number && qst.Pointer == QuestionPointer.Previous);
						}

						if (questions.Count() != 0)
						{
							ShowQuestion(questions);
						}
						else
						{
							if (Student.Action == Action.Review)
							{
								AppController.UpdateEndTest(View.UserTest);
							}
							else
							{
								GotoReviewPage();
							}
						}
					}

					break;
				case QuestionFileType.Question:
					{
						if (Student.Action != Action.Review)
						{
							if (number == 0)
							{
								TakeNextPrevQuestion(0, QuestionFileType.Question, "N");
							}
							else
							{
								Student.QuestionType = QuestionType.Unknown;
								AppController.UpdateTestStatus();
								View.PopulateEndForAllPages();
							}
						}
						else
						{
							GotoReviewPage();
						}
					}

					break;
			}
		}

		private void OnNewTest()
		{
			if (Student.TestType == TestType.Undefined)
			{
				throw new ArgumentException(string.Format("Test type {0} is undefined", Student.TestType));
			}

			if (Student.TestType == TestType.Integrated)
			{
				var clientAllowed = string.IsNullOrEmpty(View.HTTP_X_FORWARDED_FOR)
										? ValidateIpLock(new[] { View.UserHostAddress }, Student.InstitutionIpLock)
										: ValidateIpLock(new[] { View.HTTP_X_FORWARDED_FOR }, Student.InstitutionIpLock);

				if (!clientAllowed)
				{
					AppController.ShowPage(PageDirectory.AccessDenied, null, null);
				}
			}

			Student.QuizOrQBank = TestType.Quiz;

			// Fix for NRSNGOPT-267. This is a hack to avoid the error.
			// Fixing it properly would require drastic changes to this page.
			if (Student.ProductId == 1)
			{
				var tests = AppController.GetUserTests(Student.UserId, Student.TestId);
				if (tests.Count() > 0)
				{
					// Test has already been created. Redirect the user to the test.
					// Simulating Resume mode from TestReview page.
					TraceHelper.WriteTraceError(TraceToken, "Duplication of IT Test attempted.");

					var previouslyCreatedTest = tests.FirstOrDefault();
					Student.Action = Action.Resume;
					Student.UserTestId = previouslyCreatedTest.UserTestId;
					Student.QuizOrQBank = TestType.Quiz;
					Student.TestId = Student.TestId;
					Student.SuspendType = previouslyCreatedTest.SuspendType;
					Student.NumberOfQuestions = 0;
					AppController.ShowPage(PageDirectory.Resume, null, null);
				}
			}

			var newTest = AppController.CreateTest();
			int userTestId = newTest.UserTestId;
			TraceHelper.Create(TraceToken, "Created Test for User.")
				.Add("User Test Id", userTestId.ToString())
				.Write();
			if (newTest.NumberOfQuestions != 0 && userTestId > 0)
			{
				Student.NumberOfQuestions = newTest.NumberOfQuestions;
				Student.UserTestId = userTestId;
				View.Remaining = newTest.TimeRemaining;
				TakeNextPrevQuestion(0, newTest.Questions[1].FileType, "N");
			}
			else
			{
				AppController.ShowPage(PageDirectory.IntroError, null, null);
			}
		}

		private IList<UserAnswer> PopulateAnswers(QuestionType type)
		{
			IList<UserAnswer> answers = null;
			if (type == QuestionType.MultiChoiceSingleAnswer)
			{
				answers = View.PopulateMultipleChoice();
			}

			if (type == QuestionType.MultiChoiceMultiAnswer)
			{
				answers = View.PopulateMultipleChoiceMultiSelect();
			}

			if (type == QuestionType.Number)
			{
				answers = View.PopulateFillIn();
			}

			if (type == QuestionType.Hotspot || type == QuestionType.Order)
			{
				answers = null;
			}

			return answers;
		}

		private void PopulateFields(Question question)
		{
			if (question.FileType == QuestionFileType.Disclaimer)
			{
				Student.QuestionType = QuestionType.Unknown;
				View.PopulateDisclamer(question);
			}

			if (question.FileType == QuestionFileType.Intro)
			{
				Student.QuestionType = QuestionType.Unknown;
				var testIntroQuestions =
					TestQuestionsById.Where(
						qst => qst.FileType == question.FileType && qst.QuestionNumber > question.QuestionNumber);
				View.PopulateIntroduction(question, testIntroQuestions.Count() == 0);
			}

			if (question.FileType == QuestionFileType.TutorialItem)
			{
				Student.QuestionType = QuestionType.Unknown;
				var tutorialQuestions =
					TestQuestionsById.Where(qst => qst.FileType == QuestionFileType.TutorialItem && qst.Active == 1);
				Student.NumberOfQuestions = tutorialQuestions.Count();
				tutorialQuestions =
					TestQuestionsById.Where(
						qst => qst.FileType == question.FileType && qst.QuestionNumber < question.QuestionNumber);
				View.PopulateTutorial(question, tutorialQuestions.Count() == 0);
			}

			if (question.FileType == QuestionFileType.Question)
			{
				PopulateQuestion(question);
			}

			if (question.FileType == QuestionFileType.EndItem)
			{
				Student.QuestionType = QuestionType.Unknown;
				var testEndQuestions =
					TestQuestionsById.Where(
						qst => qst.FileType == question.FileType && qst.QuestionNumber > question.QuestionNumber);
				View.PopulateEnd(question, testEndQuestions.Count() == 0);
			}
		}

		private void PopulateQuestion(Question question)
		{
			var firstQuestions = UserTestQuestionsById.Where(qst => qst.QuestionNumber < question.QuestionNumber);
			var lippinCotts = AppController.GetLippincottAssignedInQuestion();

			if (Student.Action == Action.Remediation)
			{
				View.PopulateRemediation(question, firstQuestions.Count() == 0);
				if (question.RemediationId != 0)
				{
					string explaination = AppController.GetRemediationExplainationByID(question.RemediationId);
					View.ShowLippincott(lippinCotts, explaination);
				}
			}
			else
			{
				Student.NumberOfQuestions = Student.QuizOrQBank == TestType.Quiz
												? GetQuestionsCount()
												: UserTestQuestionsById.Count();
				if (question.RemediationId != 0)
				{
					string explaination = AppController.GetRemediationExplainationByID(question.RemediationId);
					View.RemediationHtml = explaination;
				}

				View.PopulateQuestions(question, lippinCotts, firstQuestions.Count() == 0);
				View.PopulateAlternateTextDetails(question);
				var answers = AppController.GetAnswersForQuestion();
				if (Student.Action != Action.Review)
				{
					if (View.Resume)
					{
						View.SetAnswerTrack(UserTestQuestionsById.FirstOrDefault(qst => qst.Id == Student.QuestionId), answers.FirstOrDefault(ans => ans.QID == Student.QuestionId) != null);
					}
				}

				var answersById = AppController.GetUserAnswerByID();
				if ((ConfigurationManager.AppSettings["ScrambleITAnswers"].Trim().ToLower() == "yes") && (Student.ProductId == 1) && (question.Type == QuestionType.MultiChoiceSingleAnswer || question.Type == QuestionType.MultiChoiceMultiAnswer))
				{
					var scrambledAnswersById = ScrambleAnswerChoice(answersById.ToList());
					for (var i = 0; i < scrambledAnswersById.Count; i++)
					{
						scrambledAnswersById[i].ScrambledAIndex = answersById.ToArray()[i].AIndex;
					}

					answersById = scrambledAnswersById.Where(ans => ans.AType == 1).ToArray();
					var scrambledDisplayOrder = string.Join(", ", from item in answersById select item.AIndex);
					AppController.UpdateScrambledAnswerChoice(scrambledDisplayOrder, Student.UserTestId,
																  answersById.FirstOrDefault().QID);
				}
				else
				{
					answersById = answersById.Where(ans => ans.AType == 1).OrderBy(key => key.AIndex).ToArray();
				}

				switch (question.Type)
				{
					case QuestionType.MultiChoiceSingleAnswer:
						{
							Student.QuestionType = QuestionType.MultiChoiceSingleAnswer;
							View.FillMultipleChoiceFields(answersById,
														  AppController.GetQuestionExhibitByID(), answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.MultiChoiceMultiAnswer:
						{
							Student.QuestionType = QuestionType.MultiChoiceMultiAnswer;
							View.FillMultipleChoiceMultiSelectFields(answersById,
																	 AppController.GetQuestionExhibitByID(),
																	 answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.Hotspot:
						{
							Student.QuestionType = QuestionType.Hotspot;
							var hotSpotAnswers = AppController.GetHotSpotAnswerByID();
							hotSpotAnswers =
								hotSpotAnswers.Where(ans => ans.AType == 1).OrderBy(key => key.AIndex);
							View.FillHotSpotFields(answers,
													  UserTestQuestionsById.FirstOrDefault(
														  qst => qst.Id == Student.QuestionId), hotSpotAnswers);
							View.PopulateAlternateTextDetails(answers,
												   UserTestQuestionsById.FirstOrDefault(
													   qst => qst.Id == Student.QuestionId), hotSpotAnswers, Student);
						}

						break;
					case QuestionType.Number:
						{
							Student.QuestionType = QuestionType.Number;
							View.FillTheBlankFields(answersById, answers);
							View.PopulateAlternateTextDetails(answersById, answers, Student);
						}

						break;
					case QuestionType.Order:
						{
							Student.QuestionType = QuestionType.Order;
							View.FillTheMatchFields(answersById,
														UserTestQuestionsById.FirstOrDefault(
															qst => qst.Id == Student.QuestionId));
							View.PopulateAlternateTextDetails(answersById,
														UserTestQuestionsById.FirstOrDefault(
															qst => qst.Id == Student.QuestionId), Student);
						}

						break;
				}
			}
		}

		private int GetQuestionsCount()
		{
			if (Student.NumberOfQuestions <= 0)
			{
				Student.NumberOfQuestions = AppController.GetTestQuestionsCount();
			}

			return Student.NumberOfQuestions;
		}

		private Question PopulateQuestionOBJ(QuestionType type)
		{
			var obj = new Question
						  {
							  UserTestId = Student.UserTestId,
							  Id = Convert.ToInt32(View.QuestionId),
							  Correct = View.CorrectQuestion,
							  ExplanationTime = 0,
							  TimeSpendForQuestion =
								  string.IsNullOrEmpty(View.TimerCount) ? 0 : Convert.ToInt32(View.TimerCount)
						  };

			int sec = 0;
			string[] arrT = View.Timerup.Split(new[] { ':' });
			if (arrT.Length == 3)
			{
				sec = Convert.ToInt32(arrT[2]) + (60 * Convert.ToInt32(arrT[1])) + (3600 * Convert.ToInt32(arrT[0]));
			}

			if (arrT.Length == 2)
			{
				sec = Convert.ToInt32(arrT[1]) + (60 * Convert.ToInt32(arrT[0]));
			}

			obj.RemediationTime = sec;
			obj.AnswserTrack = View.AnswerTrack;
			obj.AnswerChanges = string.Empty;
			obj.OrderedIndexes = string.Empty;

			if (type == QuestionType.Order)
			{
				obj.Correct = 2;
				obj.AnswerChanges = string.Empty;
				if (String.Compare(View.BrowserType, "IE7", StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(View.BrowserType, "IE8", StringComparison.OrdinalIgnoreCase) == 0)
				{
					string ans = View.RequireResponse;
					string ord;

					if (!ans.Trim().Equals(string.Empty))
					{
						int startpoz = ans.IndexOf("orderedIndexes=\"") + 16;
						int endpoz = ans.IndexOf("correct=\"");
						string answer = string.Empty;
						if (startpoz > 0)
						{
							ord = ans.Substring(startpoz, endpoz - startpoz).Trim();
							answer = StringFunctions.Left(ord, ord.Length - 1);
						}

						startpoz = ans.IndexOf("correct=\"") + 9;
						ord = ans.Substring(startpoz, 1).Trim();
						obj.OrderedIndexes = answer;
						if (ord.Trim().Equals("1"))
						{
							obj.Correct = 1;
						}
						else if (ord.Trim().Equals("0"))
						{
							obj.Correct = 0;
						}
						else
						{
							obj.Correct = 2;
						}
					}
				}
				else
				{
					IEnumerable<AnswerChoice> answerChoices = AppController.GetAnswers(View.QuestionId.ToInt(), 1).OrderBy(o => o.Aindex);
					string correctAnswerOrder = string.Join(",", from item in answerChoices select item.InitialPosition);
					string ans = View.RequireResponse.TrimEnd(',');
					if (ans.Trim() == correctAnswerOrder.Trim())
					{
						obj.Correct = 1;
					}
					else if (ans.Trim().Length == correctAnswerOrder.Trim().Length)
					{
						obj.Correct = 0;
					}
					else
					{
						obj.Correct = 2;
					}

					obj.OrderedIndexes = ans;
				}
			}

			if (type == QuestionType.Hotspot)
			{
				obj.Correct = 2;
				obj.AnswerChanges = string.Empty;

				string ans = View.RequireResponse;
				string ord, ordXo, ordYo;
				string ansX = string.Empty;
				string cor1;
				if (!ans.Trim().Equals(string.Empty) && ans.Contains("x='") && ans.Contains("y='"))
				{
					int pozX = ans.IndexOf("x='") + 3;
					int pozY = ans.IndexOf("y='");

					if (pozX > 0)
					{
						ord = ans.Substring(pozX, pozY - pozX).Trim();
						ansX = ord.Replace("'", string.Empty).Trim();
					}

					int pozCor1 = ans.IndexOf("correct='");
					ord = ans.Substring(pozY + 3, pozCor1 - pozY - 3).Trim();
					var ansY = ord.Replace("'", string.Empty).Trim();
					int pozXo = ans.IndexOf("xo='");

					ord = ans.Substring(pozCor1 + 9, pozXo - pozCor1 - 9).Trim();
					cor1 = ord.Replace("'", string.Empty).Trim();

					int pozYo = ans.IndexOf("yo='");

					ord = ans.Substring(pozXo + 4, pozYo - pozXo - 4).Trim();
					ordXo = ord.Replace("'", string.Empty).Trim();
					int pozCor2 = ans.IndexOf("correcto='");

					ord = ans.Substring(pozYo + 4, pozCor2 - pozYo - 4).Trim();
					ordYo = ord.Replace("'", string.Empty).Trim();

					int pozEnd = ans.IndexOf("/>");

					ord = ans.Substring(pozCor2 + 10, pozEnd - pozCor2 - 10).Trim();
					string cor2 = ord.Replace("'", string.Empty).Trim();

					obj.OrderedIndexes = ansX + "," + ansY;

					if (cor1 == "1" && cor2 == "0")
					{
						if (ordXo != "-1" && ordYo != "-1")
						{
							obj.AnswerChanges = "IC";
						}
					}

					if (cor1 == "0" && cor2 == "1")
					{
						obj.AnswerChanges = "CI";
					}

					if (cor1 == "0" && cor2 == "0")
					{
						if (ordXo != "-1" && ordYo != "-1")
						{
							obj.AnswerChanges = "II";
						}
					}

					if (cor1.Trim().Equals("1"))
					{
						obj.Correct = 1;
					}
					else if (cor1.Trim().Equals("0"))
					{
						obj.Correct = 0;
					}
					else
					{
						obj.Correct = 2;
					}
				}
			}

			if (type == QuestionType.MultiChoiceSingleAnswer)
			{
				obj.AnswserTrack = View.AnswerTrack;
				string track = View.AnswerTrack;
				string[] arr = track.Split(new[] { '/' });
				string step = string.Empty;

				if (arr.Length == 2)
				{
					step = arr[1] == View.A1 ? "C" : "I";
				}

				if (arr.Length > 2)
				{
					string last = arr[arr.Length - 1];
					string prelast = arr[arr.Length - 2];

					if (prelast == View.A1)
					{
						step = "CI";
					}
					else
					{
						step = last == View.A1 ? "IC" : "II";
					}
				}

				obj.AnswerChanges = step;
			}

			return obj;
		}

		private void ShowMessageCtrl()
		{
			View.ShowMessageCtrl(false);
			switch (Student.Action)
			{
				case Action.Review:
					if (View.CheckIsQBankQuestion)
					{
						if (!AppController.IsTest74Question())
						{
							View.ShowMessageCtrl(true);
						}
					}

					break;
			}
		}

		private void TakeNextPrevQuestion(int number, QuestionFileType ftype, string action)
		{
			IEnumerable<Question> questions = new List<Question>().ToArray();
			CurrentTrace = TraceHelper.Create(TraceToken, "TakeNextPrevQuestion Block")
				.Add("Sl #", number.ToString())
				.Add("ftype", ftype.ToString())
				.Add("action", action);

			var nextPrevQuestions = (ftype == QuestionFileType.Question || ftype == QuestionFileType.Unknown) ? GetNextPrevQuestions(number, ftype, false) : GetPrevNextQuestionInTest(number, ftype);
			switch (action)
			{
				case "N":
					questions = nextPrevQuestions.Where(
						qst => qst.Pointer == QuestionPointer.Next);
					break;
				case "P":
					questions = nextPrevQuestions.Where(
						qst => qst.Pointer == QuestionPointer.Previous);
					break;
				case "C":
					{
						questions = nextPrevQuestions.Where(qst => qst.Pointer == QuestionPointer.Current);
						if (ftype == QuestionFileType.Question || ftype == QuestionFileType.Unknown)
						{
							// unknown could be disclaimer
							if (questions.Count() > 0 && Student.Action == Action.Remediation)
							{
								IEnumerable<Question> questions1 = questions;
								View.HideShowPreviousIncorrectButton(
									nextPrevQuestions.Where(
										qst => qst.Pointer == QuestionPointer.Previous).Count() > 0);
							}
						}
					}

					break;
			}

			if (questions.Count() != 0)
			{
				ShowQuestion(questions);
			}
			else
			{
				NextPrevQuestions(number, ftype, action);
			}

			ShowMessageCtrl();
			CurrentTrace.Write();
		}

		private void ViewNextPreviewIncorrectQuestion(int number, QuestionFileType ftype, string action)
		{
			IEnumerable<Question> questions = null;
			CurrentTrace = TraceHelper.Create(TraceToken, "ViewNextPreviewIncorrectQuestion Block")
				.Add("Sl #", number.ToString())
				.Add("ftype", ftype.ToString())
				.Add("action", action);
			var nextPrevIncorrectQuestions = (ftype == QuestionFileType.Question || ftype == QuestionFileType.Unknown) ? GetNextPrevQuestions(number, ftype, true) : GetPrevNextQuestionInTest(number, ftype).Where(qst => qst.Active == 1);

			switch (action)
			{
				case "N":
					questions = nextPrevIncorrectQuestions.Where(
						qst => qst.Pointer == QuestionPointer.Next);
					break;
				case "P":
					questions = nextPrevIncorrectQuestions.Where(
						qst => qst.Pointer == QuestionPointer.Previous);
					break;
				case "C":
					questions = nextPrevIncorrectQuestions.Where(qst => qst.Pointer == QuestionPointer.Current);
					break;
			}

			if (questions.Count() != 0)
			{
				if (ftype == QuestionFileType.Question || ftype == QuestionFileType.Unknown)
				{
					if (Student.Action == Action.Remediation)
					{
						IEnumerable<Question> questions1 = questions;
						View.HideShowPreviousIncorrectButton(nextPrevIncorrectQuestions.Where(qst => qst.Pointer == QuestionPointer.Previous).Count() > 0);
					}
				}

				ShowQuestion(questions);
			}
			else
			{
				NextPrevQuestions(number, ftype, action);
			}

			ShowMessageCtrl();
		}

		private bool WriteQuestionInTheTest()
		{
			var type = (QuestionType)Enum.Parse(typeof(QuestionType), View.QuestionTypeText);
			var answers = PopulateAnswers(type);
			var question = PopulateQuestionOBJ(type);

			TraceHelper.Create(TraceToken, "Saving Test Answer")
			.Add("User Test Id", question.UserTestId.ToString())
			.Add("Question Id", question.Id.ToString())
			.Add("Answer Track", question.AnswserTrack)
			.Write();

			return AppController.SaveQuestionInTheUserTest(question, answers, View.UserTest);
		}

		private void ShowQuestion(IEnumerable<Question> questions)
		{
			var question = questions.FirstOrDefault();
			Student.QuestionId = question.Id;
			View.PopulateFields(question);
			PopulateFields(question);
			if (CurrentTrace != null)
			{
				CurrentTrace.Add("Question Id", question.Id.ToString())
					.Add("Question Type", question.Type.ToString());
			}
		}

		private List<UserAnswer> ScrambleAnswerChoice(List<UserAnswer> inputList)
		{
			var randomList = new List<UserAnswer>();
			var r = new Random();
			while (inputList.Count() > 0)
			{
				int randomIndex = r.Next(0, inputList.Count());
				randomList.Add(inputList[randomIndex]);
				inputList.RemoveAt(randomIndex);
			}

			return randomList;
		}

	    #endregion Methods
	}
}