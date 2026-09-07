import sys
import json
from faster_whisper import WhisperModel


def report_progress(percent):
    percent = max(0, min(100, int(percent)))
    print(f"PROGRESS:{percent}", file=sys.stderr, flush=True)


def main():
    if len(sys.argv) < 4:
        print(
            "Usage: transcribe_whisper.py <audio_path> <language> <model_size>",
            file=sys.stderr,
            flush=True
        )
        sys.exit(1)

    audio_path = sys.argv[1]
    language = sys.argv[2]
    model_size = sys.argv[3]

    lang_arg = None if language == "auto" else language

    # Start at 0%
    report_progress(0)

    # Load Whisper model
    model = WhisperModel(
        model_size,
        device="cpu",
        compute_type="int8"
    )

    # Start transcription
    segments, info = model.transcribe(
        audio_path,
        language=lang_arg
    )

    duration = info.duration or 1.0

    text_parts = []
    confidences = []

    last_reported = -1

    for segment in segments:

        # Store transcription text
        text_parts.append(segment.text)

        # Store confidence
        if segment.avg_logprob is not None:
            confidences.append(segment.avg_logprob)

        # Calculate transcription progress
        percent = int(
            min(segment.end / duration, 1.0) * 100
        )

        # Only report when percentage changes
        if percent != last_reported:
            report_progress(percent)
            last_reported = percent

    # Make absolutely sure we finish at 100%
    if last_reported != 100:
        report_progress(100)

    # Create final result
    result = {
        "text": " ".join(text_parts).strip(),
        "confidence": (
            sum(confidences) / len(confidences)
            if confidences
            else None
        ),
        "language": info.language
    }

    # IMPORTANT:
    # Final JSON goes to STDOUT.
    # Progress goes to STDERR.
    print(json.dumps(result), flush=True)


if __name__ == "__main__":
    try:
        main()

    except Exception as e:
        print(
            f"ERROR:{str(e)}",
            file=sys.stderr,
            flush=True
        )
        sys.exit(1)