import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import { AppointmentService } from './appointment.service';
import { AppointmentDTO } from '../_models/appointmentDTO';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionPrintService {

  constructor(private appointmentService: AppointmentService) { }

  downloadPrescriptionById(id: number): void {
    this.appointmentService.getAppointmentById(id).subscribe(
      (appointment: AppointmentDTO) => {
        this.generatePrescriptionPDF(appointment);
      },
      (e) => {
        console.log('Error: ' + e);
      }
    );
  }

  private generatePrescriptionPDF(appointment: AppointmentDTO): void {
    const pdf = new jsPDF();

    // Helper function to format date as "25-Aug-2025"
    const formatDate = (dateString: string): string => {
      const date = new Date(dateString);
      const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
      return `${date.getDate().toString().padStart(2, '0')}-
              ${months[date.getMonth()]}-
              ${date.getFullYear()}`;
    };

    // Header
    pdf.setFontSize(20);
    pdf.setFont('helvetica', 'bold');
    pdf.text('Prescription Report', 20, 30);

    // Patient info section
    pdf.setFontSize(12);
    pdf.setFont('helvetica', 'normal');
    pdf.text(`Patient:`, 20, 50);
    pdf.text(appointment.patientName, 60, 50);
    pdf.text(`Doctor:`, 20, 60);
    pdf.text(appointment.doctorName, 60, 60);
    pdf.text(`Date:`, 20, 70);
    pdf.text(formatDate(appointment.appointmentDate), 60, 70);
    pdf.text(`Visit Type:`, 20, 80);
    pdf.text(appointment.visitType, 60, 80);

    // Prescriptions section
    pdf.setFontSize(16);
    pdf.setFont('helvetica', 'bold');
    pdf.text('Prescriptions', 20, 110);

    // Table setup
    const startX = 20;
    const startY = 130;
    const rowHeight = 20;
    const colWidths = [60, 45, 40, 40]; // Increased widths: Medicine, Dosage, Start Date, End Date
    const tableWidth = colWidths.reduce((sum, width) => sum + width, 0);

    // Helper function to truncate text to fit within column width
    const truncateText = (text: string, maxWidth: number): string => {
      pdf.setFontSize(9);
      if (pdf.getTextWidth(text) <= maxWidth) return text;

      while (pdf.getTextWidth(text + '...') > maxWidth && text.length > 0) {
        text = text.slice(0, -1);
      }
      return text + '...';
    };

    // Table header
    pdf.setFillColor(240, 240, 240); // Light gray background
    pdf.rect(startX, startY - 15, tableWidth, 15, 'F');

    pdf.setFont('helvetica', 'bold');
    pdf.setFontSize(9);
    pdf.text('Medicine', startX + 2, startY - 5);
    pdf.text('Dosage', startX + colWidths[0] + 2, startY - 5);
    pdf.text('Start Date', startX + colWidths[0] + colWidths[1] + 2, startY - 5);
    pdf.text('End Date', startX + colWidths[0] + colWidths[1] + colWidths[2] + 2, startY - 5);

    // Header borders
    pdf.setDrawColor(0, 0, 0);
    pdf.setLineWidth(0.5);
    pdf.rect(startX, startY - 15, tableWidth, 15); // Header border

    // Vertical lines for header
    let currentX = startX;
    for (let i = 0; i < colWidths.length - 1; i++) {
      currentX += colWidths[i];
      pdf.line(currentX, startY - 15, currentX, startY);
    }

    // Table rows
    pdf.setFont('helvetica', 'normal');
    pdf.setFontSize(9);
    let currentY = startY;

    appointment.prescriptions.forEach((prescription, index) => {
      // Alternate row colors
      if (index % 2 === 0) {
        pdf.setFillColor(250, 250, 250);
        pdf.rect(startX, currentY, tableWidth, rowHeight, 'F');
      }

      // Row data with text truncation
      const medicineText = truncateText(prescription.medicineName, colWidths[0] - 4);
      const dosageText = truncateText(prescription.dosage, colWidths[1] - 4);
      const startDateText = formatDate(prescription.startDate);
      const endDateText = formatDate(prescription.endDate);

      pdf.text(medicineText, startX + 2, currentY + 12);
      pdf.text(dosageText, startX + colWidths[0] + 2, currentY + 12);
      pdf.text(startDateText, startX + colWidths[0] + colWidths[1] + 2, currentY + 12);
      pdf.text(endDateText, startX + colWidths[0] + colWidths[1] + colWidths[2] + 2, currentY + 12);

      // Row border
      pdf.rect(startX, currentY, tableWidth, rowHeight);

      // Vertical lines for each row
      currentX = startX;
      for (let i = 0; i < colWidths.length - 1; i++) {
        currentX += colWidths[i];
        pdf.line(currentX, currentY, currentX, currentY + rowHeight);
      }

      currentY += rowHeight;
    });

    // Download the PDF
    pdf.save(`prescription-${appointment.patientName}-${formatDate(appointment.appointmentDate)}.pdf`);

  }

}