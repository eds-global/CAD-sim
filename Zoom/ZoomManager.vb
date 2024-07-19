Imports System.Net.Mime.MediaTypeNames

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports ZwSoft.ZwCAD.DatabaseServices
Imports ZwSoft.ZwCAD.Geometry
Imports ZwSoft.ZwCAD.ApplicationServices
Imports ZwSoft.ZwCAD.EditorInput

Imports ZwSoft.ZwCAD

Public Class ZoomManager

    'Public Shared Sub Zoom(ByVal pMin As Point3d, ByVal pMax As Point3d,
    '                    ByVal pCenter As Point3d, ByVal dFactor As Double)
    '    '' Get the current document and database
    '    Dim acDoc As Document = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
    '    Dim acCurDb As Database = acDoc.Database

    '    Dim nCurVport As Integer = System.Convert.ToInt32(ZwSoft.ZwCAD.ApplicationServices.Application.GetSystemVariable("CVPORT"))

    '    '' Get the extents of the current space when no points 
    '    '' or only a center point is provided
    '    '' Check to see if Model space is current
    '    If acCurDb.TileMode = True Then
    '        If pMin.Equals(New Point3d()) = True And
    '                pMax.Equals(New Point3d()) = True Then

    '            pMin = acCurDb.Extmin
    '            pMax = acCurDb.Extmax
    '        End If
    '    Else
    '        '' Check to see if Paper space is current
    '        If nCurVport = 1 Then
    '            If pMin.Equals(New Point3d()) = True And
    '                    pMax.Equals(New Point3d()) = True Then

    '                pMin = acCurDb.Pextmin
    '                pMax = acCurDb.Pextmax
    '            End If
    '        Else
    '            '' Get the extents of Model space
    '            If pMin.Equals(New Point3d()) = True And
    '                    pMax.Equals(New Point3d()) = True Then

    '                pMin = acCurDb.Extmin
    '                pMax = acCurDb.Extmax
    '            End If
    '        End If
    '    End If

    '    '' Start a transaction
    '    Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()
    '        '' Get the current view
    '        Using acView As ViewTableRecord = acDoc.Editor.GetCurrentView()
    '            Dim eExtents As Extents3d

    '            '' Translate WCS coordinates to DCS
    '            Dim matWCS2DCS As Matrix3d
    '            matWCS2DCS = Matrix3d.PlaneToWorld(acView.ViewDirection)
    '            matWCS2DCS = Matrix3d.Displacement(acView.Target - Point3d.Origin) * matWCS2DCS
    '            matWCS2DCS = Matrix3d.Rotation(-acView.ViewTwist,
    '                                               acView.ViewDirection,
    '                                               acView.Target) * matWCS2DCS

    '            '' If a center point is specified, define the min and max 
    '            '' point of the extents
    '            '' for Center and Scale modes
    '            If pCenter.DistanceTo(Point3d.Origin) <> 0 Then
    '                pMin = New Point3d(pCenter.X - (acView.Width / 2),
    '                                       pCenter.Y - (acView.Height / 2), 0)

    '                pMax = New Point3d((acView.Width / 2) + pCenter.X,
    '                                       (acView.Height / 2) + pCenter.Y, 0)
    '            End If

    '            '' Create an extents object using a line
    '            Using acLine As Line = New Line(pMin, pMax)
    '                eExtents = New Extents3d(acLine.Bounds.Value.MinPoint,
    '                                             acLine.Bounds.Value.MaxPoint)
    '            End Using

    '            '' Calculate the ratio between the width and height of the current view
    '            Dim dViewRatio As Double
    '            dViewRatio = (acView.Width / acView.Height)

    '            '' Tranform the extents of the view
    '            matWCS2DCS = matWCS2DCS.Inverse()
    '            eExtents.TransformBy(matWCS2DCS)

    '            Dim dWidth As Double
    '            Dim dHeight As Double
    '            Dim pNewCentPt As Point2d

    '            '' Check to see if a center point was provided (Center and Scale modes)
    '            If pCenter.DistanceTo(Point3d.Origin) <> 0 Then
    '                dWidth = acView.Width
    '                dHeight = acView.Height

    '                If dFactor = 0 Then
    '                    pCenter = pCenter.TransformBy(matWCS2DCS)
    '                End If

    '                pNewCentPt = New Point2d(pCenter.X, pCenter.Y)
    '            Else '' Working in Window, Extents and Limits mode
    '                '' Calculate the new width and height of the current view
    '                dWidth = eExtents.MaxPoint.X - eExtents.MinPoint.X
    '                dHeight = eExtents.MaxPoint.Y - eExtents.MinPoint.Y

    '                '' Get the center of the view
    '                pNewCentPt = New Point2d(((eExtents.MaxPoint.X + eExtents.MinPoint.X) * 0.5),
    '                                             ((eExtents.MaxPoint.Y + eExtents.MinPoint.Y) * 0.5))
    '            End If

    '            '' Check to see if the new width fits in current window
    '            If dWidth > (dHeight * dViewRatio) Then dHeight = dWidth / dViewRatio

    '            '' Resize and scale the view
    '            If dFactor <> 0 Then
    '                acView.Height = dHeight * dFactor
    '                acView.Width = dWidth * dFactor
    '            End If

    '            '' Set the center of the view
    '            acView.CenterPoint = pNewCentPt

    '            '' Set the current view
    '            acDoc.Editor.SetCurrentView(acView)
    '        End Using

    '        '' Commit the changes
    '        acTrans.Commit()
    '    End Using
    'End Sub

    'Public Shared Sub ZoomExtents()
    '    '' Zoom to the extents of the current space
    '    Dim doc As Document = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
    '    Dim db As Database = doc.Database

    '    Dim MaxExt As New Point3d
    '    MaxExt = doc.Database.Extmax
    '    Dim MinExt As New Point3d
    '    MinExt = doc.Database.Extmin

    '    Zoom(MinExt, MaxExt, New Point3d(), 1.01075)
    'End Sub

    Private Shared Sub ZoomWin(ed As Editor, min As Point3d, max As Point3d)
        Dim min2d As New Point2d(min.X, min.Y)
        Dim max2d As New Point2d(max.X, max.Y)

        Dim view As New ViewTableRecord()

        view.CenterPoint = min2d + ((max2d - min2d) / 2.0)
        view.Height = max2d.Y - min2d.Y
        view.Width = max2d.X - min2d.X
        ed.SetCurrentView(view)
    End Sub

    Public Shared Sub Zoom2Handle(s As String)
        Try
            Dim doc As ZwSoft.ZwCAD.ApplicationServices.Document = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

            Dim ed As Editor = doc.Editor

            Dim db As Database = doc.Database

            Using tr As Transaction = db.TransactionManager.StartTransaction()

                Dim ln As Long = Convert.ToInt64(s, 16)

                Dim hn As New Handle(ln)

                Dim id As ObjectId = db.GetObjectId(False, hn, 0)

                Dim obj As DBObject = tr.GetObject(id, OpenMode.ForRead)

                Dim ent As Entity = TryCast(obj, Entity)

                Dim ext As Extents3d = ent.GeometricExtents

                ZoomWin(ed, ext.MinPoint, ext.MaxPoint)

                tr.Commit()

            End Using

        Catch ex As System.Exception

        End Try
    End Sub


End Class
