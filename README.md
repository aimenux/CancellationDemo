# CancellationDemo
*Task cancellation strategies*

> 4 strategies are implemented in order to cancel a long runing work
>> `ManualCancellation` : cancel long running cancellable work when pressing a cancel key
>
>> `TimeoutCancellation` : cancel long running cancellable work after a timeout delay
>
>> `WrappedCancellation` : wrap long running **non** cancellable work using a cancellation token
>
>> `CompositeCancellation` : cancel long running cancellable work using multiple cancellation tokens
