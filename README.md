# background-task-host

You can run tasks in the background and allow for them to complete, fault etc.. before app shutdown.

```
<6>16:25:24 BackgroundTaskHost[0] BackgroundTaskHost started
<6>16:25:24 Microsoft.Hosting.Lifetime[14] Now listening on: http://localhost:5110
<6>16:25:24 Microsoft.Hosting.Lifetime[0] Application started. Press Ctrl+C to shut down.
<6>16:25:24 Microsoft.Hosting.Lifetime[0] Hosting environment: Development
<6>16:25:24 Microsoft.Hosting.Lifetime[0] Content root path: /Users/alex.harper/RiderProjects/background-task-host
<6>16:25:28 BackgroundTaskHost[0] Added new Task 1
<6>16:25:35 BackgroundTaskHost[0] Added new Task 2
<6>16:25:35 BackgroundTaskHost[0] Added new Task 3
^C<6>16:25:37 Microsoft.Hosting.Lifetime[0] Application is shutting down...
<6>16:25:37 BackgroundTaskHost[0] BackgroundTaskHost stopping...
<6>16:25:37 BackgroundTaskHost[0] Waiting for tasks to complete: 1,2,3
<6>16:25:42 BackgroundTaskHost[0] Waiting for tasks to complete: 1,2,3
<6>16:25:47 BackgroundTaskHost[0] Waiting for tasks to complete: 1,2,3
<6>16:25:52 BackgroundTaskHost[0] Waiting for tasks to complete: 1,2,3
<6>16:25:57 BackgroundTaskHost[0] Waiting for tasks to complete: 1,2,3
<6>16:25:58 BackgroundTaskHostExample[0] Long running task finished doing a thing
<6>16:25:58 BackgroundTaskHost[0] Task 1 completed with state RanToCompletion
<6>16:26:02 BackgroundTaskHost[0] Waiting for tasks to complete: 2,3
<6>16:26:05 BackgroundTaskHostExample[0] Long running task finished doing a thing
<6>16:26:05 BackgroundTaskHost[0] Task 2 completed with state RanToCompletion
<6>16:26:05 BackgroundTaskHostExample[0] Long running task finished doing a thing
<6>16:26:05 BackgroundTaskHost[0] Task 3 completed with state RanToCompletion
<6>16:26:07 BackgroundTaskHost[0] BackgroundTaskHost stopped

```