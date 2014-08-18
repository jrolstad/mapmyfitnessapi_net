using System;

namespace mapmyfitnessapi_sdk.workouts
{
    public class WorkoutApiRequest:MapMyFitnessApiRequest
    {
        public int UserId { get; set; }

        public int? WorkoutId { get; set; }

        public int? ActivityType { get; set; }

        public DateTime? StartedAfter { get; set; }

        public DateTime? StartedBefore { get; set; }

        public bool? TimeSeries { get; set; }

        public WorkoutApiRequest WithUserId(int id)
        {
            UserId = id;

            return this;
        }

        public WorkoutApiRequest WithWorkoutId(int id)
        {
            WorkoutId = id;

            return this;
        }

        public WorkoutApiRequest WithActivityType(int id)
        {
            ActivityType = id;

            return this;
        }

        public WorkoutApiRequest WithStartedAfter(DateTime value)
        {
            StartedAfter = value;

            return this;
        }

        public WorkoutApiRequest WithStartedBefore(DateTime value)
        {
            StartedBefore = value;

            return this;
        }

        public WorkoutApiRequest WithTimeSeries(bool value)
        {
            TimeSeries = value;

            return this;
        }

       
    }
}