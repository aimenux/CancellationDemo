# CancellationDemo

*Cancellation strategies demo*

> 4 strategies are implemented in order to cancel a long running work
>
>> `ManualCancellation` : cancel long running cancellable work when pressing a cancel key
>
>> `TimeoutCancellation` : cancel long running cancellable work after a timeout delay
>
>> `WrappedCancellation` : wrap long running **non** cancellable work using a cancellation token
>
>> `CompositeCancellation` : cancel long running cancellable work using multiple cancellation tokens

*Cancellation callbacks demo*

> 3 examples of registered callbacks that raise exception before or after cancellation
>
>> `CallbackRaiseExceptionAfterCancellation` : raise exception in a callback registered after cancellation
>
>> `CallbackRaiseExceptionBeforeCancellation` : raise exception in a callback registered before cancellation
>
>> `CallbacksRaiseExceptionsBeforeCancellation` : raise exception in multiple callbacks registered before cancellation